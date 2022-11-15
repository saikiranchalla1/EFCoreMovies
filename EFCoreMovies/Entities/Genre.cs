using System.ComponentModel.DataAnnotations;

namespace EFCoreMovies.Entities
{
    public class Genre
    {
        public int Id { get; set; } // styl 1
        /*public int GenreID { get; set; }*/ // style 2

        // [Key]
        // public int Identifier { get; set; } // throws an error of keyless entity
        public string Name { get; set; }
    }
}
