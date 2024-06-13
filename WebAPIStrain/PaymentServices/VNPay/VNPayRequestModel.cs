namespace WebAPIStrain.PaymentServices.VNPay
{
    public class VNPayRequestModel
    {
        public string FullName { get; set; }
        public string Description { get; set; }
        public long Amount { get; set; }
        public string OrderId { get; set; }
    }
}
