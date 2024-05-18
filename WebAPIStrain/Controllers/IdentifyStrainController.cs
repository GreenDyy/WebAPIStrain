using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIStrain.Models;
using WebAPIStrain.Services;

namespace WebAPIStrain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentifyStrainController : ControllerBase
    {
        private readonly IIdentifyStrainRepository _identifyStrainRepository;

        public IdentifyStrainController(IIdentifyStrainRepository identifyStrainRepository)
        {
            _identifyStrainRepository = identifyStrainRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_identifyStrainRepository.GetAll());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{idEmployee}/{idStrain}")]
        public IActionResult GetById(string idEmployee, int idStrain)
        {
            try
            {
                var data = _identifyStrainRepository.GetById(idEmployee, idStrain);
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

        [HttpDelete("{idEmployee}/{idStrain}")]
        public IActionResult Delete(string idEmployee, int idStrain)
        {
            try
            {
                if (_identifyStrainRepository.Delete(idEmployee, idStrain))
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

        [HttpPut("{idEmployee}/{idStrain}")]
        public IActionResult Update(string idEmployee, int idStrain, IdentifyStrainModel inputIdentifyStrain)
        {
            try
            {
                if (_identifyStrainRepository.Update(idEmployee, idStrain, inputIdentifyStrain))
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
        public IActionResult Create(IdentifyStrainModel inputIdentifyStrain)
        {
            try
            {
                _identifyStrainRepository.Create(inputIdentifyStrain);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}