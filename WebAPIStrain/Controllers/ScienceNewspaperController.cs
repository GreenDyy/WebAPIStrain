using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIStrain.Models;
using WebAPIStrain.Services;

namespace WebAPIStrain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScienceNewspaperController : ControllerBase
    {
        private readonly IScienceNewspaperRepository _scienceNewspaperRepository;

        public ScienceNewspaperController(IScienceNewspaperRepository scienceNewspaperRepository)
        {
            _scienceNewspaperRepository = scienceNewspaperRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_scienceNewspaperRepository.GetAll());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var data = _scienceNewspaperRepository.GetById(id);
                if (data != null)
                {
                    return Ok(data);
                }
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (_scienceNewspaperRepository.Delete(id))
                {
                    return NoContent();
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ScienceNewspaperModel inputScienceNewspaper)
        {
            try
            {
                if (_scienceNewspaperRepository.Update(id, inputScienceNewspaper))
                {
                    return NoContent();
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public IActionResult Create(ScienceNewspaperModel inputScienceNewspaper)
        {
            try
            {
                var newScienceNewspaper = _scienceNewspaperRepository.Create(inputScienceNewspaper);
                return CreatedAtAction(nameof(GetById), new { id = newScienceNewspaper.IdNewspaper }, newScienceNewspaper);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("GetRandom")]
        public IActionResult GetRandom()
        {
            try
            {
                return Ok(_scienceNewspaperRepository.GetRandom());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}