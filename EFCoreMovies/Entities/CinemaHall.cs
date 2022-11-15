using System.ComponentModel;

namespace EFCoreMovies.Entities
{
    public class CinemaHall
    {
        public int Id { get; set; }
        public decimal Cost { get; set; }
        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; }
        public CinemaHallType CinemaHallType { get; set; }

        public HashSet<Movie> Movies { get; set; }
    }
}
