using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using System.Reflection.PortableExecutable;
using WebAPIStrain.Models;
using WebAPIStrain.Services;

namespace WebAPIStrain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StrainController : ControllerBase
    {
        private readonly IStrainRepository _strainRepository;

        public StrainController(IStrainRepository strainRepository)
        {
            _strainRepository = strainRepository;
        }

        [HttpGet("NoPaging")]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_strainRepository.GetAll());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public IActionResult GetAll(string? search, string? sortBy, int page = 1)
        {
            try
            {
                return Ok(_strainRepository.GetAll(search, sortBy, page));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("FollowPhylum")]
        public IActionResult GetAllStrainPhylum(string? namePhylum, string? search, string? sortBy, int page = 1)
        {
            try
            {
                return Ok(_strainRepository.GetAllStrainPhylum(page, namePhylum, search, sortBy));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("FollowClass")]
        public IActionResult GetAllStrainClass(string? nameClass, string? search, string? sortBy, int page = 1)
        {
            try
            {
                return Ok(_strainRepository.GetAllStrainClass(page, nameClass, search, sortBy));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("FollowGenus")]
        public IActionResult GetAllStrainGenus(string? nameGenus, string? search, string? sortBy, int page = 1)
        {
            try
            {
                return Ok(_strainRepository.GetAllStrainGenus(page, nameGenus, search, sortBy));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("FollowSpecies")]
        public IActionResult GetAllStrainSpecies(string? nameSpecies, string? search, string? sortBy, int page = 1)
        {
            try
            {
                return Ok(_strainRepository.GetAllStrainSpecies(page, nameSpecies, search, sortBy));
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
                var strain = _strainRepository.GetById(id);
                if (strain != null)
                {
                    return Ok(strain);
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

        [HttpGet("StrainNumber/{strainNumber}")]
        public IActionResult GetByStrainNumber(string strainNumber)
        {
            try
            {
                var strain = _strainRepository.GetByStrainNumber(strainNumber);
                if (strain != null)
                {
                    return Ok(strain);
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

        [HttpGet("GetRandom")]
        public IActionResult GetRandomStrain()
        {
            try
            {
                return Ok(_strainRepository.GetRandomStrain());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("GetAllByStraiNumberAndScientificName")]
        public IActionResult GetAllByStraiNumberAndScientificName(string? search)
        {
            try
            {
                return Ok(_strainRepository.GetAllByStraiNumberAndScientificName(search));
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
                if (_strainRepository.Delete(id))
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
        public IActionResult Update(int id, StrainModel strain)
        {
            try
            {
                if (_strainRepository.Update(id, strain))
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

        [HttpPatch("{id}/StrainNumber")]
        public IActionResult UpdateStrainNumber(int id, [FromBody] string strainNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(strainNumber))
                {
                    return BadRequest("StrainNumber cannot be null or empty.");
                }

                if (_strainRepository.UpdateStrainNumber(id, strainNumber))
                {
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public IActionResult Create(StrainModel strain)
        {
            try
            {
                var strans = _strainRepository.Create(strain);
                return Ok(strans);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("GetAllStrainByTheEmployee")]
        public IActionResult GetAllStrainByTheEmployee(string idEmployee)
        {
            try
            {
                return Ok(_strainRepository.GetAllStrainByTheEmployee(idEmployee));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
