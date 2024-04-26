using System.Reflection;
using Microsoft.EntityFrameworkCore;
using VirtoCommerce.Platform.Data.Infrastructure;
using VirtoCommerce.WhiteLabeling.Data.Models;

namespace VirtoCommerce.WhiteLabeling.Data.Repositories;

public class WhiteLabelingDbContext : DbContextBase
{
    private const int MaxLength128 = 128;

    public WhiteLabelingDbContext(DbContextOptions<WhiteLabelingDbContext> options)
        : base(options)
    {
    }

    protected WhiteLabelingDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<WhiteLabelingSettingEntity>().ToTable("WhiteLabelingSettings").HasKey(x => x.Id);
        modelBuilder.Entity<WhiteLabelingSettingEntity>().Property(x => x.Id).HasMaxLength(MaxLength128).ValueGeneratedOnAdd();

        switch (Database.ProviderName)
        {
            case "Pomelo.EntityFrameworkCore.MySql":
                modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("VirtoCommerce.WhiteLabeling.Data.MySql"));
                break;
            case "Npgsql.EntityFrameworkCore.PostgreSQL":
                modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("VirtoCommerce.WhiteLabeling.Data.PostgreSql"));
                break;
            case "Microsoft.EntityFrameworkCore.SqlServer":
                modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("VirtoCommerce.WhiteLabeling.Data.SqlServer"));
                break;
        }
    }
}
