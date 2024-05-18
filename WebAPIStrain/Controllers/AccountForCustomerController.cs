using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIStrain.Models;
using WebAPIStrain.Services;

namespace WebAPIStrain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountForCustomerController : ControllerBase
    {
        private readonly IAccountForCustomerRepository _accountRepository;

        public AccountForCustomerController(IAccountForCustomerRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_accountRepository.GetAll());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                var data = _accountRepository.GetById(id);
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
        public IActionResult Delete(string id)
        {
            try
            {
                if (_accountRepository.Delete(id))
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
        public IActionResult Update(string id, AccountForCustomerModel inputAccount)
        {
            try
            {
                if (_accountRepository.Update(id, inputAccount))
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
        public IActionResult Create(AccountForCustomerModel inputAccount)
        {
            try
            {
                var newAccount = _accountRepository.Create(inputAccount);
                return CreatedAtAction(nameof(GetById), new { id = newAccount.IdCustomer }, newAccount);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}