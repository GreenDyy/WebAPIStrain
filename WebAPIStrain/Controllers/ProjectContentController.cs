using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIStrain.Models;
using WebAPIStrain.Services;

namespace WebAPIStrain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectContentController : ControllerBase
    {
        private readonly IProjectContentRepository _projectContentRepository;

        public ProjectContentController(IProjectContentRepository projectContentRepository)
        {
            _projectContentRepository = projectContentRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_projectContentRepository.GetAll());
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
                var data = _projectContentRepository.GetById(id);
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
                if (_projectContentRepository.Delete(id))
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
        public IActionResult Update(int id, ProjectContentModel inputProjectContent)
        {
            try
            {
                if (_projectContentRepository.Update(id, inputProjectContent))
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
        public IActionResult Create(ProjectContentModel inputProjectContent)
        {
            try
            {
                var newProjectContent = _projectContentRepository.Create(inputProjectContent);
                return CreatedAtAction(nameof(GetById), new { id = newProjectContent.IdProjectContent }, newProjectContent);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPatch("{idProject}/statusProject")]
        public IActionResult UpdateStatusProject(string idProject, [FromBody] string status)
        {
            try
            {
                if (string.IsNullOrEmpty(status))
                {
                    return BadRequest("Cannot be null or empty.");
                }

                if (_projectContentRepository.UpdateStatusProject(idProject, status))
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
        [HttpPatch("{idProjectContent}/statusProjectContent")]
        public IActionResult UpdateStatusProjectContent(int idProjectContent, [FromBody] string status)
        {
            try
            {
                if (string.IsNullOrEmpty(status))
                {
                    return BadRequest("Cannot be null or empty.");
                }

                if (_projectContentRepository.UpdateStatusProjectContent(idProjectContent, status))
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
        [HttpGet("GetAllByIdProject")]
        public IActionResult GetAllByIdProject(string idProject)
        {
            try
            {
                return Ok(_projectContentRepository.GetAllByIdProject(idProject));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}