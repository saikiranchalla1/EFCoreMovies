using EFCoreMovies.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreMovies
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //  modelBuilder.Entity<Genre>(g => g.Identifier); <- Adding a primary key using overidden methods


            modelBuilder.Entity<Genre>().Property(p => p.Name).HasMaxLength(150).IsRequired();

            modelBuilder.Entity<Actor>().Property(p => p.Name).HasMaxLength(150).IsRequired();

            modelBuilder.Entity<Cinema>().Property(p => p.Name).HasMaxLength(150).IsRequired();

            modelBuilder.HasPostgresExtension("postgis"); // ensure that postgis extension is installed on the server

            modelBuilder.Entity<Movie>().Property(p => p.Title).HasMaxLength(150).IsRequired();

            modelBuilder.Entity<Movie>().Property(p => p.PosterURL).HasMaxLength(150).IsUnicode(false); // IsUnicode allows us to define the characters that we want to accept.
            // Add migration afater the above step and checkout the type of PosterURL. It should be varchar


            modelBuilder.Entity<CinemaOffer>().Property(p => p.DiscountPercentage).HasPrecision(5, 2);
            modelBuilder.Entity<CinemaOffer>().Property(p => p.Begin).HasColumnType("date");
            modelBuilder.Entity<CinemaOffer>().Property(p => p.End).HasColumnType("date");

            modelBuilder.Entity<CinemaHall>().Property(p => p.Cost).HasPrecision(5, 2);
        }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }

        public DbSet<Cinema> Cinemas { get; set; }

        // add-migration before adding the following code and check that the Movie table is already created without specifying the DbSet

        public DbSet<Movie> Movies { get; set; }

        public DbSet<CinemaHall> CinemaHalls { get; set; }
        
    }

}
