using System;
using Microsoft.EntityFrameworkCore;
using ShowReminder.Data.Entity;

namespace ShowReminder.Data
{
    public class ShowContext : DbContext
    {
        public DbSet<Show> Shows { get; set; }

        public ShowContext(DbContextOptions<ShowContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureShow(modelBuilder);
            ConfigureEpisode(modelBuilder);
        }

        protected void ConfigureBasePropertiesForEntity<T>(ModelBuilder modelBuilder) where T : BaseEntity
        {
            modelBuilder.Entity<T>()
                .Property(e => e.Id)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<T>()
                .Property(e => e.CreateDate)
                .HasColumnName("CREATE_DATE")
                .IsRequired()
                .ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<T>()
               .Property(e => e.UpdateDate)
               .HasColumnName("UPDATE_DATE")
               .IsRequired()
               .ValueGeneratedOnAddOrUpdate();
        }

        protected void ConfigureShow(ModelBuilder modelBuilder)
        {
            ConfigureBasePropertiesForEntity<Show>(modelBuilder);

            //add the info for creating show
            modelBuilder.Entity<Show>()
                .ToTable("SHOW")
                .HasKey(s => s.Id);

            modelBuilder.Entity<Show>()
                .Property(s => s.TvdbId)
                .HasColumnName("TVDB_ID")
                .IsRequired();

            modelBuilder.Entity<Show>()
                .Property(s => s.Name)
                .HasColumnName("NAME")
                .IsRequired()
                .HasMaxLength(250);

            modelBuilder.Entity<Show>()
                .Property(s => s.FirstAiredDate)
                .HasColumnName("FIRST_AIRED_DATE");

            modelBuilder.Entity<Show>()
                .Property(s => s.AirDay)
                .HasColumnName("AIR_DAY")
                .HasMaxLength(50);

            modelBuilder.Entity<Show>()
                .Property(s => s.AirTime)
                .HasColumnName("AIR_TIME")
                .HasMaxLength(50);

            modelBuilder.Entity<Show>()
                .Property(s => s.LastEpisodeId)
                .HasColumnName("LAST_EPISODE_ID");

            modelBuilder.Entity<Show>()
               .Property(s => s.NextEpisodeId)
               .HasColumnName("NEXT_EPISODE_ID");

            modelBuilder.Entity<Show>()
                .HasOne(s => s.LastEpisode)
                .WithOne();

            modelBuilder.Entity<Show>()
                .HasOne(s => s.NextEpisode)
                .WithOne();
        }

        protected void ConfigureEpisode(ModelBuilder modelBuilder)
        {
            ConfigureBasePropertiesForEntity<Episode>(modelBuilder);

            modelBuilder.Entity<Episode>()
                .ToTable("EPISODE")
                .HasKey(e => e.Id);

            modelBuilder.Entity<Episode>()
                .Property(e => e.OverallNumber)
                .HasColumnName("OVERALL_NUMBER");

            modelBuilder.Entity<Episode>()
                .Property(e => e.SeasonNumber)
                .HasColumnName("SEASON_NUMBER");

            modelBuilder.Entity<Episode>()
                .Property(e => e.EpisodeNumber)
                .HasColumnName("EPISODE_NUMBER");

            modelBuilder.Entity<Episode>()
                .Property(e => e.AirDate)
                .HasColumnName("AIR_DATE");

            modelBuilder.Entity<Episode>()
                .Property(e => e.Name)
                .HasColumnName("NAME")
                .HasMaxLength(250)
                .IsRequired();

            modelBuilder.Entity<Episode>()
                .Property(e => e.Overview)
                .HasColumnName("OVERVIEW")
                .HasMaxLength(4000);
        }
    }
}
