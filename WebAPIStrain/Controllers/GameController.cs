using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIStrain.Data;
using WebAPIStrain.Models;

namespace WebAPIStrain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        //public static List<Game> Games = new List<Game>();
        private readonly MyDbContex _context;

        public GameController(MyDbContex context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllGame()
        {
            var games = _context.Games.ToList();

            return Ok(games);
        }

        [HttpGet("{id}")]
        public IActionResult GetGame(int id)
        {
            try
            {
                var game = _context.Games.FirstOrDefault(g => g.GameId == id);
                if (game == null)
                {
                    return NotFound();
                }
                else
                    return Ok(game);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateGame(GameModel game)
        {
            try
            {
                var newGame = new Game
                {
                    GameName = game.GameName,
                    Price = game.Price,
                    Description = game.Description,
                };
                _context.Add(newGame);
                _context.SaveChanges();
                return Ok(newGame);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, GameModel gameEdit)
        {
           
            try
            {
                var game = _context.Games.SingleOrDefault(g => g.GameId == id);
                if (game == null)
                {
                    return NotFound();
                }
                else
                {
                    game.GameName = gameEdit.GameName;
                    game.Price = gameEdit.Price;
                    game.Description = gameEdit.Description;
                    _context.SaveChanges();
                    return Ok(game);
                }   
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
