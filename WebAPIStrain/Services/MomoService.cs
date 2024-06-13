using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Org.BouncyCastle.Asn1.Ocsp;
using WebAPIStrain.PaymentServices.Momo.Request;
using WebAPIStrain.PaymentServices.Momo.Response;

namespace WebAPIStrain.Services
{
    public class MomoService : IMomoService
    {
        private readonly HttpClient _httpClient;

        public MomoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //public async Task<(bool, string)> CreatePaymentLinkAsync(MomoOneTimePaymentRequest request, string paymentUrl)
        //{
        //    var jsonRequest = JsonConvert.SerializeObject(request);
        //    var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        //    var response = await _httpClient.PostAsync(paymentUrl, httpContent);
        //    var responseContent = await response.Content.ReadAsStringAsync();

        //    if (response.IsSuccessStatusCode)
        //    {
        //        return (true, responseContent);
        //    }
        //    else
        //    {
        //        return (false, responseContent);
        //    }
        //}
        //public (bool, string?) CreatePaymentLinkAsync(MomoOneTimePaymentRequest request, string paymentUrl)
        //{
        //    using HttpClient client = new HttpClient();
        //    var requestData = JsonConvert.SerializeObject(request, new JsonSerializerSettings()
        //    {
        //        ContractResolver = new CamelCasePropertyNamesContractResolver(),
        //        Formatting = Formatting.Indented,
        //    });
        //    var requestContent = new StringContent(requestData, Encoding.UTF8,
        //        "application/json");
        //    var createPaymentLinkRes = client.PostAsync(paymentUrl, requestContent)
        //        .Result;
        //    if(createPaymentLinkRes.IsSuccessStatusCode ) 
        //    {
        //        var responseContent = createPaymentLinkRes.Content.ReadAsStringAsync().Result;
        //        var responseData = JsonConvert
        //            .DeserializeObject<MomoOneTimePaymentCreateLinkResponse>(responseContent);
        //        if(responseData.resultCode == 0)
        //        {
        //            return (true, responseData.payUrl);
        //        }
        //        else
        //        {
        //            return (false, responseData.message);
        //        }
        //    }
        //    else
        //    {
        //        return (false, createPaymentLinkRes.ReasonPhrase);
        //    }
        //}
        public (bool, string?) CreatePaymentLinkAsync(MomoOneTimePaymentRequest request, string paymentUrl)
        {
            using HttpClient client = new HttpClient();
            var requestData = JsonConvert.SerializeObject(request, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented,
            });
            var requestContent = new StringContent(requestData, Encoding.UTF8, "application/json");

            var createPaymentLinkRes = client.PostAsync(paymentUrl, requestContent).Result;
            var responseContent = createPaymentLinkRes.Content.ReadAsStringAsync().Result;

            if (createPaymentLinkRes.IsSuccessStatusCode)
            {
                var responseData = JsonConvert.DeserializeObject<MomoOneTimePaymentCreateLinkResponse>(responseContent);
                if (responseData.resultCode == 0)
                {
                    return (true, responseData.payUrl);
                }
                else
                {
                    return (false, $"Error from MoMo API: {responseData.payUrl}");
                }
            }
            else
            {
                return (false, $"Lỗi 500 mẹ rồi: {paymentUrl}");
            }
        }
    }
}
