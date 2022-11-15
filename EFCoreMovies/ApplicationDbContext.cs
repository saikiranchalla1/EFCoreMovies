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
        }
        public DbSet<Genre> Genres { get; set; }
    }

}
