
using Microsoft.Extensions.Options;
using WebAPIStrain.PaymentServices.VNPay.Library;

namespace WebAPIStrain.PaymentServices.VNPay
{
    public class VNPayService : IVNPayService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public VNPayService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            {
                _configuration = configuration;
                _httpContextAccessor = httpContextAccessor;
            }
        }

        public string CreatePaymentUrl(VNPayRequestModel model)
        {
            var clientIPAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();

            var vnPayConfig = _configuration.GetSection("VNPay").Get<VNPayConfig>();
            var tick = DateTime.Now.Ticks.ToString();

            var vnpay = new VnPayLibrary();
            vnpay.AddRequestData("vnp_Version", vnPayConfig.vnp_Version);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnPayConfig.vnp_TmnCode);
            //vnpay.AddRequestData("vnp_Amount", (model.Amount * 100).ToString());
            vnpay.AddRequestData("vnp_Amount", (model.Amount).ToString());
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));

            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", clientIPAddress);
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toán cho đơn hàng:" + model.OrderId);
            vnpay.AddRequestData("vnp_OrderType", "other");
            vnpay.AddRequestData("vnp_ReturnUrl", vnPayConfig.vnp_ReturnUrl);
            vnpay.AddRequestData("vnp_TxnRef", tick);

            var paymentUrl = vnpay.CreateRequestUrl(vnPayConfig.vnp_Url, vnPayConfig.vnp_HashSecret);

            return paymentUrl;
        }

        public VNPayResponseModel PaymentExcute(IQueryCollection collection)
        {
            var vnPayConfig = _configuration.GetSection("VNPay").Get<VNPayConfig>();
            var vnpay = new VnPayLibrary();
            foreach (var (key, value) in collection)
            {
                if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                {
                    vnpay.AddResponseData(key, value.ToString());
                }
            }

            var vnp_OrderId = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));
            var vnp_TransactionId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
            var vnp_SecureHash = collection.FirstOrDefault(p => p.Key == "vnp_SecureHash").Value;
            var vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            var vnp_OrderInfo = vnpay.GetResponseData("vnp_OrderInfo");

            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnPayConfig.vnp_HashSecret);
            if (!checkSignature)
            {
                return new VNPayResponseModel
                {
                    Success = false
                };
            }

            return new VNPayResponseModel
            {
                Success = true,
                PaymentMethod = "VnPay",
                OrderDescription = vnp_OrderInfo,
                OrderId = vnp_OrderId.ToString(),
                TransactionId = vnp_TransactionId.ToString(),
                Token = vnp_SecureHash,
                VnPayResponseCode = vnp_ResponseCode
            };
        }
    }
}
