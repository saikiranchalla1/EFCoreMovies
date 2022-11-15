namespace EFCoreMovies.Entities
{
    public class CinemaHall
    {
        public int Id { get; set; }
        public decimal Cost { get; set; }
        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; }
    }
}
