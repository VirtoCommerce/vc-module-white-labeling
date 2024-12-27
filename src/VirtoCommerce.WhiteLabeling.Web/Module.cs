using System;
using GraphQL;
using GraphQL.MicrosoftDI;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VirtoCommerce.Platform.Core.Modularity;
using VirtoCommerce.Platform.Core.Security;
using VirtoCommerce.Platform.Core.Settings;
using VirtoCommerce.StoreModule.Core.Model;
using VirtoCommerce.WhiteLabeling.Core;
using VirtoCommerce.WhiteLabeling.Core.Services;
using VirtoCommerce.WhiteLabeling.Data.MySql;
using VirtoCommerce.WhiteLabeling.Data.PostgreSql;
using VirtoCommerce.WhiteLabeling.Data.Repositories;
using VirtoCommerce.WhiteLabeling.Data.Services;
using VirtoCommerce.WhiteLabeling.Data.SqlServer;
using VirtoCommerce.WhiteLabeling.ExperienceApi;
using VirtoCommerce.Xapi.Core.Extensions;

namespace VirtoCommerce.WhiteLabeling.Web;

public class Module : IModule, IHasConfiguration
{
    public ManifestModuleInfo ModuleInfo { get; set; }
    public IConfiguration Configuration { get; set; }

    public void Initialize(IServiceCollection serviceCollection)
    {
        _ = new GraphQLBuilder(serviceCollection, builder =>
        {
            var assemblyMarker = typeof(AssemblyMarker);
            builder.AddGraphTypes(assemblyMarker.Assembly);
            serviceCollection.AddMediatR(assemblyMarker);
            serviceCollection.AddAutoMapper(assemblyMarker);
            serviceCollection.AddSchemaBuilders(assemblyMarker);
        });

        serviceCollection.AddDbContext<WhiteLabelingDbContext>(options =>
        {
            var databaseProvider = Configuration.GetValue("DatabaseProvider", "SqlServer");
            var connectionString = Configuration.GetConnectionString(ModuleInfo.Id) ?? Configuration.GetConnectionString("VirtoCommerce");

            switch (databaseProvider)
            {
                case "MySql":
                    options.UseMySqlDatabase(connectionString);
                    break;
                case "PostgreSql":
                    options.UsePostgreSqlDatabase(connectionString);
                    break;
                default:
                    options.UseSqlServerDatabase(connectionString);
                    break;
            }
        });

        serviceCollection.AddTransient<IWhiteLabelingRepository, WhiteLabelingRepository>();
        serviceCollection.AddTransient<Func<IWhiteLabelingRepository>>(provider => () => provider.CreateScope().ServiceProvider.GetRequiredService<IWhiteLabelingRepository>());

        serviceCollection.AddTransient<IWhiteLabelingSettingService, WhiteLabelingSettingService>();
        serviceCollection.AddTransient<IWhiteLabelingSettingSearchService, WhiteLabelingSettingSearchService>();
    }

    public void PostInitialize(IApplicationBuilder appBuilder)
    {
        var serviceProvider = appBuilder.ApplicationServices;

        // Register permissions
        var permissionsRegistrar = serviceProvider.GetRequiredService<IPermissionsRegistrar>();
        permissionsRegistrar.RegisterPermissions(ModuleInfo.Id, "WhiteLabeling", ModuleConstants.Security.Permissions.AllPermissions);

        // Register settings
        var settingsRegistrar = serviceProvider.GetRequiredService<ISettingsRegistrar>();
        settingsRegistrar.RegisterSettings(ModuleConstants.Settings.AllSettings, ModuleInfo.Id);
        settingsRegistrar.RegisterSettingsForType(ModuleConstants.Settings.StoreLevelSettings, nameof(Store));

        // Apply migrations
        using var serviceScope = serviceProvider.CreateScope();
        using var dbContext = serviceScope.ServiceProvider.GetRequiredService<WhiteLabelingDbContext>();
        dbContext.Database.Migrate();

    }

    public void Uninstall()
    {
        // Nothing to do here
    }
}
