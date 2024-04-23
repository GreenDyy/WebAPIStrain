using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIStrain.Data
{
    [Table("GameGenre")]
    public class GameGenre
    {
        public int GameId { get; set; }
        public Game Game { get; set; }

        public int GenreId { get; set; }
        public  Genre Genre { get; set; }
    }
}
