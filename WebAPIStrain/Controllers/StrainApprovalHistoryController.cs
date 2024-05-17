using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIStrain.Models;
using WebAPIStrain.Services;

namespace WebAPIStrain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StrainApprovalHistoryController : ControllerBase
    {
        private readonly IStrainApprovalHistoryRepository _strainApprovalHistoryRepository;

        public StrainApprovalHistoryController(IStrainApprovalHistoryRepository strainApprovalHistoryRepository) 
        {
            _strainApprovalHistoryRepository = strainApprovalHistoryRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_strainApprovalHistoryRepository.GetAll());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{idStrain}")]
        public IActionResult GetById(int idStrain)
        {
            try
            {
                var data = _strainApprovalHistoryRepository.GetById(idStrain);
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

        [HttpDelete("{idStrain}")]
        public IActionResult Delete(int idStrain)
        {
            try
            {
                if (_strainApprovalHistoryRepository.Delete(idStrain))
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

        [HttpPut("{idStrain}")]
        public IActionResult Update(int idStrain, StrainApprovalHistoryModel inputStrainApprovalHistory)
        {
            try
            {
                if (_strainApprovalHistoryRepository.Update(idStrain, inputStrainApprovalHistory))
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
        public IActionResult Create(StrainApprovalHistoryModel inputStrainApprovalHistory)
        {
            try
            {
                _strainApprovalHistoryRepository.Create(inputStrainApprovalHistory);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
