﻿namespace WebAPIStrain.PaymentServices.VNPay
{
    public class VNPayResponseModel
    {
        public bool Success { get; set; }
        public string OrderDescription { get; set; }
        public string OrderId { get; set; }
        public string PaymentId {get; set; }
        public string TransactionId { get; set; }
        public string Token { get; set; }
        public string VnPayResonseCode { get; set; }
        public string PaymentMethod {  get; set; }
        public string VnPayResponseCode { get; set; }
    }
}
