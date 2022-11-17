using System.ComponentModel;

namespace EFCoreMovies.Entities
{
    public class CinemaHall
    {
        public int Id { get; set; }
        public decimal Cost { get; set; }
        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; }
        public virtual CinemaHallType CinemaHallType { get; set; }

        public virtual HashSet<Movie> Movies { get; set; }
    }
}
