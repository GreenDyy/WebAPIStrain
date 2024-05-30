using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using WebAPIStrain.Entities;
using WebAPIStrain.Models;
using WebAPIStrain.Services;
using WebAPIStrain.ViewModels;

namespace WebAPIStrain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMailServiceRepository _mailServiceRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IOrderDetailRepository _orderDetailRepository;

        public OrderController(IOrderRepository orderRepository, IMailServiceRepository mailServiceRepository, ICustomerRepository customerRepository, IWebHostEnvironment webHostEnvironment, IOrderDetailRepository orderDetailRepository)
        {
            _orderRepository = orderRepository;
            _mailServiceRepository = mailServiceRepository;
            _customerRepository = customerRepository;
            _webHostEnvironment = webHostEnvironment;
            _orderDetailRepository = orderDetailRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_orderRepository.GetAll());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                var data = _orderRepository.GetById(id);
                if (data != null)
                {
                    return Ok(data);
                }
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                if (_orderRepository.Delete(id))
                {
                    return NoContent();
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, OrderModel inputOrder)
        {
            try
            {
                if (_orderRepository.Update(id, inputOrder))
                {
                    return NoContent();
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public IActionResult Create(OrderModel inputOrder)
        {
            try
            {
                var newOrder = _orderRepository.Create(inputOrder);
                return Ok(newOrder);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("SendMailOrder")]
        public async Task<IActionResult> SendMailOrder(string idOrder)
        {
            try
            {
                var newOrder = _orderRepository.GetById(idOrder);
                var inforUser = _customerRepository.GetById(newOrder.IdCustomer);
                var listOrderDetail = _orderDetailRepository.GetAllByIdOrder(newOrder.IdOrder);

                var productTemplate = await System.IO.File.ReadAllTextAsync(Path.Combine(_webHostEnvironment.ContentRootPath, "MailTemplate", "OneProduct.html"));
                var allproductHtml = ""; // Chuỗi HTML chứa tất cả các sản phẩm
                foreach (var item in listOrderDetail)
                {
                    var imageStrain = item.IdStrainNavigation?.ImageStrain;
                    var imageBase64 = imageStrain != null ? Convert.ToBase64String(imageStrain) : "";

                    var productHtml = productTemplate
                                        .Replace("{{HinhAnh}}", imageBase64)
                                        .Replace("{{TenSanPham}}", item?.IdStrainNavigation?.ScientificName?.ToString())
                                        .Replace("{{SoLuong}}", item?.Quantity?.ToString())
                                        .Replace("{{Gia}}", Convert.ToDecimal(item.Price).ToString("N0"));

                    // Thêm sản phẩm đã thay thế vào chuỗi HTML chứa tất cả các sản phẩm
                    allproductHtml += productHtml;
                }

                var subject = "Đơn hàng #DH" + newOrder.IdOrder + " đã đặt thành công";
                var templatePath = Path.Combine(_webHostEnvironment.ContentRootPath, "MailTemplate", "OrderTemplate.html");
                var message = await System.IO.File.ReadAllTextAsync(templatePath);

                // Thay thế các biến trong nội dung tệp HTML
                message = message.Replace("{{TenKhachHang}}", inforUser.FullName.ToString())
                                 .Replace("{{NgayDat}}", DateTime.Now.ToString("dd-MM-yyyy"))
                                 .Replace("{{MaDonHang}}", newOrder.IdOrder.ToString())
                                 .Replace("{{DiaChi}}", newOrder.DeliveryAddress.ToString())
                                 .Replace("{{ListOrderDetail}}", allproductHtml)
                                 .Replace("{{TongTien}}", Convert.ToDecimal(newOrder.TotalPrice - (newOrder.TotalPrice / 11)).ToString("N0"))
                                 .Replace("{{Thue}}", Convert.ToDecimal(newOrder.TotalPrice / 11).ToString("N0"))
                                 .Replace("{{ThanhTien}}", Convert.ToDecimal(newOrder.TotalPrice).ToString("N0"));

                await _mailServiceRepository.SendMailAsync(inforUser.Email, subject, message);

                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("GetAllByIdCustomer/{idCustomer}")]
        public IActionResult GetAllByIdCustomer(string idCustomer)
        {
            try
            {
                return Ok(_orderRepository.GetAllByIdCustomer(idCustomer));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}