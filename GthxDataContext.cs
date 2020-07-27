using System;
using Gthx.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
        public virtual DbSet<Factoid> Factoids { get; set; }
        public virtual DbSet<Ref> Refs { get; set; }
        public virtual DbSet<Seen> Seen { get; set; }
        public virtual DbSet<Tell> Tell { get; set; }
        public virtual DbSet<ThingiverseRef> ThingiverseRefs { get; set; }
        public virtual DbSet<Version> Version { get; set; }
        public virtual DbSet<YoutubeRef> YoutubeRefs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=GthxTest;Integrated Security=True;");
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FactoidHistory>(entity =>
            {
                entity.ToTable("factoid_history");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Dateset).HasColumnName("dateset");

                entity.Property(e => e.Item)
                    .HasColumnName("item")
                    .HasColumnType("varchar(255)")
                    //.HasCharSet("utf8mb4")
                    ;//.HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Nick)
                    .HasColumnName("nick")
                    .HasColumnType("varchar(30)")
                    //.HasCharSet("utf8mb4")
                    ;//.HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Value)
                    .HasColumnName("value")
                    .HasColumnType("varchar(512)")
                    //.HasCharSet("utf8mb4")
                    ;//.HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<Factoid>(entity =>
            {
                entity.ToTable("factoids");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Are).HasColumnName("are");

                entity.Property(e => e.Dateset)
                    .HasColumnName("dateset")
                    .HasColumnType("datetime");

                entity.Property(e => e.Item)
                    .HasColumnName("item")
                    .HasColumnType("varchar(255)")
                    //.HasCharSet("utf8mb4")
                    ;//.HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Locked).HasColumnName("locked");

                entity.Property(e => e.Nick)
                    .HasColumnName("nick")
                    .HasColumnType("varchar(30)")
                    //.HasCharSet("utf8mb4")
                    ;//.HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Value)
                    .HasColumnName("value")
                    .HasColumnType("varchar(512)")
                    //.HasCharSet("utf8mb4")
                    ;//.HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<Ref>(entity =>
            {
                entity.HasKey(e => e.Item)
                    .HasName("PRIMARY");

                entity.ToTable("refs");

                entity.Property(e => e.Item)
                    .HasColumnName("item")
                    .HasColumnType("varchar(191)")
                    //.HasCharSet("utf8mb4")
                    ;//.HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Count)
                    .HasColumnName("count")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Lastreferenced)
                    .HasColumnName("lastreferenced")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Seen>(entity =>
            {
                entity.ToTable("seen");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Channel)
                    .HasColumnName("channel")
                    .HasColumnType("varchar(30)")
                    //.HasCharSet("utf8mb4")
                    ;//.HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Message)
                    .HasColumnName("message")
                    .HasColumnType("varchar(512)")
                    //.HasCharSet("utf8mb4")
                    ;//.HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(30)")
                    //.HasCharSet("utf8mb4")
                    ;//.HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Timestamp)
                    .HasColumnName("timestamp")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Tell>(entity =>
            {
                entity.ToTable("tell");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Author)
                    .HasColumnName("author")
                    .HasColumnType("varchar(60)")
                    //.HasCharSet("utf8mb4")
                    ;//.HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.InTracked).HasColumnName("inTracked");

                entity.Property(e => e.Message)
                    .HasColumnName("message")
                    .HasColumnType("text")
                    //.HasCharSet("utf8mb4")
                    ;//.HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Recipient)
                    .HasColumnName("recipient")
                    .HasColumnType("varchar(60)")
                    //.HasCharSet("utf8mb4")
                    ;//.HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Timestamp)
                    .HasColumnName("timestamp")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<ThingiverseRef>(entity =>
            {
                entity.HasKey(e => e.Item)
                    .HasName("PRIMARY");

                entity.ToTable("thingiverseRefs");

                entity.Property(e => e.Item)
                    .HasColumnName("item")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Count)
                    .HasColumnName("count")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Lastreferenced)
                    .HasColumnName("lastreferenced")
                    .HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasColumnType("varchar(255)")
                    //.HasCharSet("utf8mb4")
                    ;//.HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<YoutubeRef>(entity =>
            {
                entity.HasKey(e => e.Item)
                    .HasName("PRIMARY");

                entity.ToTable("youtubeRefs");

                entity.Property(e => e.Item)
                    .HasColumnName("item")
                    .HasColumnType("varchar(191)")
                    //.HasCharSet("utf8mb4")
                    ;//.HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Count)
                    .HasColumnName("count")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Lastreferenced)
                    .HasColumnName("lastreferenced")
                    .HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasColumnType("varchar(255)")
                    //.HasCharSet("utf8mb4")
                    ;//.HasCollation("utf8mb4_general_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
