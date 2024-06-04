using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIStrain.Entities;
using WebAPIStrain.Models;
using WebAPIStrain.Services;

namespace WebAPIStrain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentWorkController : ControllerBase
    {
        private readonly IContentWorkRepository _contentWorkRepository;

        public ContentWorkController(IContentWorkRepository contentWorkRepository)
        {
            _contentWorkRepository = contentWorkRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_contentWorkRepository.GetAll());
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
                var data = _contentWorkRepository.GetById(id);
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
                if (_contentWorkRepository.Delete(id))
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
        public IActionResult Update(int id, ContentWorkModel inputContentWork)
        {
            try
            {
                if (_contentWorkRepository.Update(id, inputContentWork))
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
        public IActionResult Create(ContentWorkModel inputContentWork)
        {
            try
            {
                var newContentWork = _contentWorkRepository.Create(inputContentWork);
                return CreatedAtAction(nameof(GetById), new { id = newContentWork.IdContentWork }, newContentWork);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPatch("{idProjectContent}/status")]
        public IActionResult UpdateStatusProjectContent(int idProjectContent, [FromBody] string status)
        {
            try
            {
                if (string.IsNullOrEmpty(status))
                {
                    return BadRequest("Cannot be null or empty.");
                }

                if (_contentWorkRepository.UpdateStatusProjectContent(idProjectContent, status))
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

        [HttpPatch("{idContentWork}/file")]
        public IActionResult UpdateFileSaveAndName(int idContentWork, [FromBody] UpdateFileModel updateFileDto)
        {
            try
            {
                if (_contentWorkRepository.UpdateFileSaveAndName(idContentWork, updateFileDto.FileSave, updateFileDto.FileName))
                {
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("{idContentWork}/progressEmployee")]
        public IActionResult UpdateStatusContentWork(int idContentWork, [FromBody] UpdateStatusContentWorkModel updateStatusDto)
        {
            try
            {
                if (_contentWorkRepository.UpdateStatusContentWork(idContentWork, updateStatusDto.Results, updateStatusDto.EndDateActual))
                {
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("{idContentWork}/notificationNull")]
        public IActionResult UpdateNotificationNull(int idContentWork)
        {
            try
            {
                if (_contentWorkRepository.UpdateNotificationNull(idContentWork))
                {
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetAllByIdEmployee")]
        public IActionResult GetAllByIdEmployee(string idEmployee)
        {
            try
            {
                return Ok(_contentWorkRepository.GetAllByIdEmployee(idEmployee));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}