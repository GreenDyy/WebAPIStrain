﻿using Microsoft.AspNetCore.Http;
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

        [HttpPost]
        public IActionResult Create(CustomerModel customer)
        {
            _customerRepository.Create(customer);
            return Ok();
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
    }
}
