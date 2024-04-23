using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIStrain.Data;

namespace WebAPIStrain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly MyDbContex _context;
        public GenreController(MyDbContex context) 
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var genres = _context.Genres.ToList();

            return Ok(genres);
        }
    }
}
