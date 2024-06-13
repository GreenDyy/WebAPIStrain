
namespace WebAPIStrain.PaymentServices.VNPay
{
    public interface IVNPayService
    {
      
        string CreatePaymentUrl(VNPayRequestModel model);
        VNPayResponseModel PaymentExcute(IQueryCollection collection);

    }
}
