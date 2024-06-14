using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPIStrain.Models;
using WebAPIStrain.Services;
using WebAPIStrain.ViewModels;

namespace WebAPIStrain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly AppSettings _appSettings;

        public CustomerController(ICustomerRepository customerRepository, IOptionsMonitor<AppSettings> optionsMonitor)
        {
            _customerRepository = customerRepository;
            _appSettings = optionsMonitor.CurrentValue;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_customerRepository.GetAll());
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
                var data = _customerRepository.GetById(id);
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
                if (_customerRepository.Delete(id))
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
        public IActionResult Update(string id, CustomerModel customer)
        {
            try
            {
                if (_customerRepository.Update(id, customer))
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

        [HttpPut("ChangePass/{id}")]
        public IActionResult ChangePass(string id, CustomerModel customer)
        {
            try
            {
                if (_customerRepository.ChangePass(id, customer))
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

        [HttpPut("UpdateDataNoPass/{id}")]
        public IActionResult UpdateDataNoPass(string id, CustomerModel customer)
        {
            try
            {
                if (_customerRepository.UpdateDataNoPass(id, customer))
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
        public IActionResult Create(CustomerModel customer)
        {
            if (_customerRepository.Create(customer) != null)
                return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost("Login")]
        public IActionResult Login(Login account)
        {
            var customer = _customerRepository.Login(account);
            if (customer != null)
            {
                //ok thì cấp token
                //return Ok(new ApiResponse
                //{
                //    Success = true,
                //    Message = "Authenticate Success",
                //    Data = customer
                //});
                return Ok(customer);
            }
            else
            {
                //return Ok(new ApiResponse
                //{
                //    Success = false,
                //    Message = "Sai tài khoản hoặc mật khẩu"
                //});
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //[HttpPost("Login")]
        //public IActionResult Login(Login account)
        //{
        //    var customer = _customerRepository.Login(account);
        //    if (customer != null)
        //    {
        //        //ok thì cấp token
        //        return Ok(new ApiResponse
        //        {
        //            Success = true,
        //            Message = "Authenticate Success",
        //            Data = GenerateToken(customer)
        //        });
        //    }
        //    else
        //    {
        //        return Ok(new ApiResponse
        //        {
        //            Success = false,
        //            Message = "Sai tài khoản hoặc mật khẩu"
        //        });
        //    }
        //}

        private string GenerateToken(CustomerVM customer)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("UserName", customer.Username),
                    new Claim("Password", customer.Password),
                    //roles
                    new Claim("TokenId", Guid.NewGuid().ToString())
                }),

                Expires = DateTime.UtcNow.AddMinutes(1),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey
                (secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtSecurityTokenHandler.CreateToken(tokenDescription);
            return jwtSecurityTokenHandler.WriteToken(token);
        }
        [HttpPut("reset-pass")]
        public IActionResult ResetPassword(string email, string newPass)
        {
            try
            {
                if (_customerRepository.ResetPassword(email, newPass) == true)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost("CheckExistEmail")]
        public IActionResult CheckExistEmail(string email)
        {
            try
            {
                bool emailExists = _customerRepository.CheckExistEmail(email);

                if (emailExists)
                {
                    return Ok(new
                    {
                        status = 1,
                        message = "Email đã tồn tại"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        status = 0,
                        message = "Email có thể sử dụng được"
                    });
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost("CheckExistEmailWithoutSelf")]
        public IActionResult CheckExistEmailWithoutSelf(string email, string idCustomer)
        {
            try
            {
                bool emailExists = _customerRepository.CheckExistEmailWithoutSelf(email, idCustomer);

                if (emailExists)
                {
                    return Ok(new
                    {
                        status = 1,
                        message = "Email này không được phép dùng"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        status = 0,
                        message = "Email có thể sử dụng được"
                    });
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("CheckExistUserName")]
        public IActionResult CheckExistUserName(string userName)
        {
            try
            {
                bool emailExists = _customerRepository.CheckExistUserName(userName);

                if (emailExists)
                {
                    return Ok(new
                    {
                        status = 1,
                        message = "Tên người dùng đã tồn tại"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        status = 0,
                        message = "Tên người dùng có thể sử dụng được"
                    });
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
