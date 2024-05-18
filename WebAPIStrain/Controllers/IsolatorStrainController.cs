using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIStrain.Models;
using WebAPIStrain.Services;

namespace WebAPIStrain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IsolatorStrainController : ControllerBase
    {
        private readonly IIsolatorStrainRepository _isolatorStrainRepository;

        public IsolatorStrainController(IIsolatorStrainRepository isolatorStrainRepository)
        {
            _isolatorStrainRepository = isolatorStrainRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_isolatorStrainRepository.GetAll());
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
                var data = _isolatorStrainRepository.GetById(idEmployee, idStrain);
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
                if (_isolatorStrainRepository.Delete(idEmployee, idStrain))
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
        public IActionResult Update(string idEmployee, int idStrain, IsolatorStrainModel inputIsolatorStrain)
        {
            try
            {
                if (_isolatorStrainRepository.Update(idEmployee, idStrain, inputIsolatorStrain))
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
        public IActionResult Create(IsolatorStrainModel inputIsolatorStrain)
        {
            try
            {
                _isolatorStrainRepository.Create(inputIsolatorStrain);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
