using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using VirtoCommerce.WhiteLabeling.Data.Repositories;

namespace VirtoCommerce.WhiteLabeling.Data.SqlServer;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<WhiteLabelingDbContext>
{
    public WhiteLabelingDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<WhiteLabelingDbContext>();
        var connectionString = args.Length != 0 ? args[0] : "Server=(local);User=virto;Password=virto;Database=VirtoCommerce3;";

        builder.UseSqlServer(
            connectionString,
            options => options.MigrationsAssembly(GetType().Assembly.GetName().Name));

        return new WhiteLabelingDbContext(builder.Options);
    }
}
