using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIStrain.PaymentServices.VNPay;

namespace WebAPIStrain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VNPayController : ControllerBase
    {
        private readonly IVNPayService _vnpayService;

        public VNPayController(IVNPayService vnpayService)
        {
            _vnpayService = vnpayService;
        }

        [HttpPost("CreatePaymentUrl")]
        public IActionResult CreatePaymentUrl([FromBody] VNPayRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Thực hiện tạo URL thanh toán
            var paymentUrl = _vnpayService.CreatePaymentUrl(model);

            return Ok(paymentUrl);
        }

        [HttpPost("PaymentExcute")]
        public IActionResult PaymentExcute()
        {
            var collection = HttpContext.Request.Query;

            // Xử lý phản hồi từ VNPay
            var response = _vnpayService.PaymentExcute(collection);

            return Ok(response);
        }
    }
}
