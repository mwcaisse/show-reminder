using System;
using Microsoft.EntityFrameworkCore;
using ShowReminder.Data.Entity;

namespace ShowReminder.Data
{
    public class DataContext : DbContext
    {
        public virtual DbSet<TrackedShow> Shows { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureTrackedShow(modelBuilder);
            ConfigureTrackedEpisode(modelBuilder);

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

        protected void ConfigureTrackedShow(ModelBuilder modelBuilder)
        {
            ConfigureBasePropertiesForEntity<TrackedShow>(modelBuilder);

            //add the info for creating show
            modelBuilder.Entity<TrackedShow>()
                .ToTable("SHOW")
                .HasKey(s => s.Id);

            modelBuilder.Entity<TrackedShow>()
                .Property(s => s.TvdbId)
                .HasColumnName("TVDB_ID")
                .IsRequired();

            modelBuilder.Entity<TrackedShow>()
                .Property(s => s.Name)
                .HasColumnName("NAME")
                .IsRequired()
                .HasMaxLength(250);

            modelBuilder.Entity<TrackedShow>()
                .Property(s => s.FirstAiredDate)
                .HasColumnName("FIRST_AIRED_DATE");

            modelBuilder.Entity<TrackedShow>()
                .Property(s => s.AirDay)
                .HasColumnName("AIR_DAY")
                .HasMaxLength(50);

            modelBuilder.Entity<TrackedShow>()
                .Property(s => s.AirTime)
                .HasColumnName("AIR_TIME")
                .HasMaxLength(50);

            modelBuilder.Entity<TrackedShow>()
                .Property(s => s.LastEpisodeId)
                .HasColumnName("LAST_EPISODE_ID");

            modelBuilder.Entity<TrackedShow>()
               .Property(s => s.NextEpisodeId)
               .HasColumnName("NEXT_EPISODE_ID");

            modelBuilder.Entity<TrackedShow>()
                .HasOne(s => s.LastEpisode)
                .WithOne();

            modelBuilder.Entity<TrackedShow>()
                .HasOne(s => s.NextEpisode)
                .WithOne();
        }

        protected void ConfigureTrackedEpisode(ModelBuilder modelBuilder)
        {
            ConfigureBasePropertiesForEntity<TrackedEpisode>(modelBuilder);

            modelBuilder.Entity<TrackedEpisode>()
                .ToTable("EPISODE")
                .HasKey(e => e.Id);

            modelBuilder.Entity<TrackedEpisode>()
                .Property(e => e.OverallNumber)
                .HasColumnName("OVERALL_NUMBER");

            modelBuilder.Entity<TrackedEpisode>()
                .Property(e => e.SeasonNumber)
                .HasColumnName("SEASON_NUMBER");

            modelBuilder.Entity<TrackedEpisode>()
                .Property(e => e.EpisodeNumber)
                .HasColumnName("EPISODE_NUMBER");

            modelBuilder.Entity<TrackedEpisode>()
                .Property(e => e.AirDate)
                .HasColumnName("AIR_DATE");

            modelBuilder.Entity<TrackedEpisode>()
                .Property(e => e.Name)
                .HasColumnName("NAME")
                .HasMaxLength(250)
                .IsRequired();

            modelBuilder.Entity<TrackedEpisode>()
                .Property(e => e.Overview)
                .HasColumnName("OVERVIEW")
                .HasMaxLength(4000);
        }
    }
}
