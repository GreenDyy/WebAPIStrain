using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using WebAPIStrain.Data;

namespace WebAPIStrain.Data
{
    [Table("GAME")]
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GameId { get; set; }
        public string GameName { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }

        public ICollection<GameGenre> GameGenres {get;set;}

    }
}
