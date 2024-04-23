using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPIStrain.Data
{
    [Table("Genre")]
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public ICollection<GameGenre> GameGenres { get; set; }
    }
}
