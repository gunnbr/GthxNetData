using Gthx.Core;
using Microsoft.EntityFrameworkCore;

namespace GthxData
{
    public partial class GthxDataContext : DbContext
    {
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
    }
}
