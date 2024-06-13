using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using WebAPIStrain.PaymentServices.Momo.Config;
using WebAPIStrain.PaymentServices.Momo.Request;
using WebAPIStrain.Services;

namespace WebAPIStrain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MomoController : ControllerBase
    {
        private readonly IMomoService _momoService;
        private readonly IConfiguration _configuration;

        public MomoController(IMomoService momoService, IConfiguration configuration)
        {
            _momoService = momoService;
            _configuration = configuration;
        }

        [HttpPost("momo-payment")]
        public async Task<IActionResult> MomoPayment()
        {
            try
            {
                var momoConfig = _configuration.GetSection("Momo").Get<MomoConfig>();

                var request = new MomoOneTimePaymentRequest
                {
                    orderInfo = "Thanh toán qua ví MoMo",
                    partnerCode = momoConfig.PartnerCode,
                    redirectUrl = momoConfig.ReturnUrl,
                    ipnUrl = momoConfig.IpnUrl,
                    amount = 5000,
                    orderId = Guid.NewGuid().ToString(),
                    requestId = Guid.NewGuid().ToString(),
                    extraData = null,
                    autoCapture = true,
                    lang = "vi",
                    requestType = "captureWallet"
                };

                // Tạo chữ ký
                request.MakeSignature(momoConfig.AccessKey, momoConfig.SecretKey);

                var (success, message) = _momoService.CreatePaymentLinkAsync(request, momoConfig.PaymentUrl);

                if (success)
                {
                    return Ok(new { success = true, payUrl = message });
                }
                else
                {
                    return Ok(new { success = false, error = message });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, error = ex.Message });
            }
        }
    }
}
