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
        public IActionResult GetAll(string? search, string? sortBy, string statusSell = "Yes", int page = 1)
        {
            try
            {
                return Ok(_strainRepository.GetAll(search, sortBy, statusSell, page));
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

        [HttpPost]
        public IActionResult Create(StrainModel strain)
        {
            try
            {
                _strainRepository.Create(strain);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
