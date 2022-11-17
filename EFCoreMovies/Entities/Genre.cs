using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreMovies.Entities
{
    [Table("GenresTbl", Schema = "Movies")]
    public class Genre
    {

        public int Id { get; set; } // styl 1
        /*public int GenreID { get; set; }*/ // style 2

        // [Key]
        // public int Identifier { get; set; } // throws an error of keyless entity

        // [StringLength(150)] <- or do a similar thing in the ApplicationDbContext
        // [Required] <= or do similary in ApplicationDbContext

        [Column("GenreName")]
        public string Name { get; set; }

        public virtual HashSet<Movie> Movies { get; set; }
    }
}
