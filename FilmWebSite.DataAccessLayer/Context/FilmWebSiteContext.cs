using FilmWebSite.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmWebSite.DataAccessLayer.Context
{
    public class FilmWebSiteContext :DbContext
    {
        public FilmWebSiteContext(DbContextOptions<FilmWebSiteContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<FilmActor> FilmActors { get; set; }
        public DbSet<FilmCategory> FilmCategories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FilmCategory>()
                .HasKey(fc => new { fc.FilmId,fc.CategoryId });

            modelBuilder.Entity<FilmCategory>()
                .HasOne(fc => fc.Film)
                .WithMany(f => f.FilmCategories)
                .HasForeignKey(fc => fc.FilmId);

            modelBuilder.Entity<FilmCategory>()
                .HasOne(fc => fc.Category)
                .WithMany(c => c.FilmCategories)
                .HasForeignKey(fc => fc.CategoryId);

            modelBuilder.Entity<FilmActor>()
                .HasKey(fa => new { fa.FilmId, fa.ActorId });

            modelBuilder.Entity<FilmActor>()
                .HasOne(fa => fa.Film)
                .WithMany(f => f.FilmActors)
                .HasForeignKey(fa => fa.FilmId);

            modelBuilder.Entity<FilmActor>()
                .HasOne(fa => fa.Actor)
                .WithMany(a => a.FilmActors)
                .HasForeignKey(fa => fa.ActorId);

        }
    }
}
