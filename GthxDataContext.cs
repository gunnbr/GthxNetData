using Gthx.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using System;

namespace GthxData
{
    public partial class GthxDataContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<GthxDataContext> _logger;

        public GthxDataContext(IConfiguration configuration, ILogger<GthxDataContext> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

#if false
        public GthxDataContext(DbContextOptions<GthxDataContext> options)
            : base(options)
        {
        
        }
#endif
        public virtual DbSet<FactoidHistory> FactoidHistory { get; set; }
        public virtual DbSet<Factoid> Factoid { get; set; }
        public virtual DbSet<Ref> Ref { get; set; }
        public virtual DbSet<Seen> Seen { get; set; }
        public virtual DbSet<Tell> Tell { get; set; }
        public virtual DbSet<ThingiverseRef> ThingiverseRef { get; set; }
        public virtual DbSet<YoutubeRef> YoutubeRef { get; set; }

        public static readonly ILoggerFactory ConsoleLoggerFactory
         = LoggerFactory.Create(builder =>
         {
             builder.AddFilter((category, level) =>
               category == DbLoggerCategory.Database.Command.Name
               && level == LogLevel.Information)
           .AddConsole();
         });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("GthxDb");
            _logger.LogInformation("ConnectionString is: {connectionString}", connectionString);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLoggerFactory(ConsoleLoggerFactory)
                    .UseSqlServer(connectionString);
            }
        }
    }
}
