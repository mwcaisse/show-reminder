using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore.Extensions;
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
                .Property(s => s.LastEpisodeDate)
                .HasColumnName("LAST_EPISODE_DATE");

            modelBuilder.Entity<Show>()
               .Property(s => s.NextEpisodeDate)
               .HasColumnName("NEXT_EPISODE_DATE");
        }
    }
}
