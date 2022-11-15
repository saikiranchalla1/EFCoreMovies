using EFCoreMovies.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EFCoreMovies
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }


        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateTime>().HaveColumnType("date");

            configurationBuilder.Properties<string>().HaveMaxLength(150);
            //base.ConfigureConventions(configurationBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }

        public DbSet<Cinema> Cinemas { get; set; }

        // add-migration before adding the following code and check that the Movie table is already created without specifying the DbSet

        public DbSet<Movie> Movies { get; set; }

        public DbSet<CinemaHall> CinemaHalls { get; set; }

        public DbSet<MovieActor> MoviesActors { get; set; }
        
    }

}
