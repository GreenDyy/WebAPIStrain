using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIStrain.Models;
using WebAPIStrain.Services;

namespace WebAPIStrain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorNewspaperController : ControllerBase
    {
        private readonly IAuthorNewspaperRepository _authorNewspaperRepository;

        public AuthorNewspaperController(IAuthorNewspaperRepository authorNewspaperRepository)
        {
            _authorNewspaperRepository = authorNewspaperRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_authorNewspaperRepository.GetAll());
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
                var data = _authorNewspaperRepository.GetById(id);
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
                if (_authorNewspaperRepository.Delete(id))
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
        public IActionResult Update(int id, AuthorNewspaperModel inputAuthorNewspaper)
        {
            try
            {
                if (_authorNewspaperRepository.Update(id, inputAuthorNewspaper))
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
        public IActionResult Create(AuthorNewspaperModel inputAuthorNewspaper)
        {
            try
            {
                var newAuthorNewspaper = _authorNewspaperRepository.Create(inputAuthorNewspaper);
                return CreatedAtAction(nameof(GetById), new { id = newAuthorNewspaper.IdNewspaper }, newAuthorNewspaper);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}