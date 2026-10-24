using GthxData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Gthx.Data
{
    /// <summary>
    /// This is a design time factory just to allow the command line dotnet ef
    /// tools to work properly and be able to configure the GthxDataContext
    /// for the SQL Server target.
    /// </summary>
    public class GthxDataContextFactory : IDesignTimeDbContextFactory<GthxDataContext>
    {
        public GthxDataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<GthxDataContext>();
            optionsBuilder.UseSqlServer("Data Source=unused", x => x.MigrationsAssembly("SqlServerMigrations"));

            return new GthxDataContext(optionsBuilder.Options);
        }
    }
}
