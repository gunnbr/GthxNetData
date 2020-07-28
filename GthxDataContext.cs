using Gthx.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace GthxData
{
    public partial class GthxDataContext : DbContext
    {
        public GthxDataContext()
        {
        }

        public GthxDataContext(DbContextOptions<GthxDataContext> options)
            : base(options)
        {
        }

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
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=GthxTest;Integrated Security=True;");
            }
        }
    }
}
