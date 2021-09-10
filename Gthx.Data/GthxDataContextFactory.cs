using GthxData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace Gthx.Data
{
    /// <summary>
    /// This is a design time factory just to allow the command line dotnet ef
    /// tools to work properly and be able to configure the GthxDataContext
    /// for both MariaDB and SQL Server targets.
    /// </summary>
    public class GthxDataContextFactory : IDesignTimeDbContextFactory<GthxDataContext>
    {
        public GthxDataContext CreateDbContext(string[] args)
        {
            if (args.Length < 2 || !args[0].Equals("--provider", StringComparison.CurrentCultureIgnoreCase))
            {
                throw new Exception("Please specify a provider to use with '--provider SqlSever' or '--provider MariaDB'");
            }

            var optionsBuilder = new DbContextOptionsBuilder<GthxDataContext>();

            if (args[1].Equals("sqlserver", StringComparison.CurrentCultureIgnoreCase))
            {
                optionsBuilder.UseSqlServer("Data Source=unused", x => x.MigrationsAssembly("SqlServerMigrations"));
            }
            else if (args[1].Equals("mariadb", StringComparison.CurrentCultureIgnoreCase))
            {
                optionsBuilder.UseMySql("Data Source=unused", new MariaDbServerVersion(new Version(10, 3, 29)),
                    x => x.MigrationsAssembly("MariaDbMigrations"));
            }
            else
            {
                throw new Exception($"Unsupported provider '{args[1]}'. Supported providers are 'sqlserver' and 'mariadb'");
            }

            return new GthxDataContext(optionsBuilder.Options);
        }
    }
}
