using System;
using System.Security.Cryptography;
using System.Text;

namespace WebAPIStrain.PaymentServices.Momo.Request
{
    public class MomoOneTimePaymentRequest
    {
        public string partnerCode { get; set; }
        public string requestId { get; set; }
        public long amount { get; set; }
        public string orderId { get; set; }
        public string orderInfo { get; set; }
        public string redirectUrl { get; set; }
        public string ipnUrl { get; set; }
        public string requestType { get; set; }
        public string extraData { get; set; }
        public object userInfo { get; set; }
        public bool autoCapture { get; set; }
        public string lang { get; set; }
        public string signature { get; set; }

        public MomoOneTimePaymentRequest(string partnerCode, string requestId, long amount, string orderId, string orderInfo,
            string redirectUrl, string ipnUrl, string requestType, string extraData, string userInfo, bool autoCapture,
            string lang, string signature)
        {
            this.partnerCode = partnerCode;
            this.requestId = requestId;
            this.amount = amount;
            this.orderId = orderId;
            this.orderInfo = orderInfo;
            this.redirectUrl = redirectUrl;
            this.ipnUrl = ipnUrl;
            this.requestType = requestType;
            this.extraData = extraData;
            this.userInfo = userInfo;
            this.autoCapture = autoCapture;
            this.lang = lang;
            this.signature = signature;
        }
        public MomoOneTimePaymentRequest()
        { }


        public void MakeSignature(string accessKey, string secretKey)
        {
            string rawSignature = $"accessKey={accessKey}&amount={amount}&extraData={extraData}&ipnUrl={ipnUrl}&orderId={orderId}&orderInfo={orderInfo}&partnerCode={partnerCode}&redirectUrl={redirectUrl}&requestId={requestId}&requestType={requestType}";
            HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
            byte[] hashMessage = hmac.ComputeHash(Encoding.UTF8.GetBytes(rawSignature));
            this.signature = BitConverter.ToString(hashMessage).Replace("-", "").ToLower();
        }
    }
}
