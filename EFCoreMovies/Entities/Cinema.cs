using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace EFCoreMovies.Entities
{
    public class Cinema
    {
        public int Id { get; set; }

        public string Name { get; set; }

       /* [Precision(precision: 9, scale: 2)] // or can be done in the ApplicationDbCOntext using HasPrecision
        public decimal Price { get; set; } // add-migration after this and notice the warning*/


        public Point Location { get; set; }

        public virtual CinemaOffer CinemaOffer { get; set; } // this is called a Navigation Property

        public virtual List<CinemaHall> CinemaHalls { get; set; }
    }
}
