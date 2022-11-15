using Microsoft.EntityFrameworkCore;

namespace EFCoreMovies.Entities
{
    public class Cinema
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Precision(precision: 9, scale: 2)] // or can be done in the ApplicationDbCOntext using HasPrecision
        public decimal Price { get; set; } // add-migration after this and notice the warning




    }
}
