using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIStrain.Services;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace WebAPIStrain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMailServiceRepository _mailServiceRepository;
        private readonly IMemoryCache _memoryCache;

        public AuthController(IMailServiceRepository mailServiceRepository, IMemoryCache memoryCache)
        {
            _mailServiceRepository = mailServiceRepository;
            _memoryCache = memoryCache;
        }

        [HttpPost("send-otp")]
        public async Task<IActionResult> SendOtp(string toEmail)
        {
            var otp = GenerateOtp();
            var subject = "Xin chào,";
            var message = $"Đây là mã OTP của bạn dùng để đặt lại mật khẩu: {otp}";

            await _mailServiceRepository.SendMailAsync(toEmail, subject, message);


            SaveOtpToCache(toEmail, otp);
            return Ok(new
            {
                Success = true,
                Message = "OTP gửi thành công",
            });
        }

        private string GenerateOtp()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        private void SaveOtpToCache(string email, string otp)
        {
            _memoryCache.Set(email, otp, TimeSpan.FromMinutes(1)); // hết hạn sau 1p
        }

        [HttpPost("verify-otp")]
        public IActionResult VerifyOtp(string email, string otp)
        {
            if (_memoryCache.TryGetValue(email, out string? cachedOtp))
            {
                if (cachedOtp == otp)
                {
                    return Ok(new
                    {
                        Status = 1,
                        Message = "Mã OTP hợp lệ",
                    });
                }
                else
                {
                    // Mã OTP không khớp
                    return Ok(new
                    {
                        Status = 0,
                        Message = "Mã OTP không hợp lệ",
                    });
                }
            }
            else
            {
                // Không tìm thấy mã OTP cho địa chỉ email này
                return Ok(new
                {
                    Status = -1,
                    Message = "OTP đã hết hạn",
                });
            }
        }
    }
}
