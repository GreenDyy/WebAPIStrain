using WebAPIStrain.PaymentServices.Momo.Request;

namespace WebAPIStrain.Services
{
    public interface IMomoService
    {
        //Task<(bool, string)> CreatePaymentLinkAsync(MomoOneTimePaymentRequest request, string paymentUrl);
        (bool, string?) CreatePaymentLinkAsync(MomoOneTimePaymentRequest request, string paymentUrl);
    }
}
