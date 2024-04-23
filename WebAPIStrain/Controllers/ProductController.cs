using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIStrain.Data;
//using WebAPIStrain.Models;

namespace WebAPIStrain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public static List<Product> products = new List<Product>();
        private readonly MyDbContex _context;

        public ProductController(MyDbContex context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            //var pros = _context.Products.ToList();
            //products = _context.Products.ToList();
            return Ok(products);
        }

        [HttpGet("{MaSanPham}")]
        public IActionResult GetByID(int MaSanPham)
        {
            try
            {
                var product = products.SingleOrDefault(p => p.MaSanPham == MaSanPham);
                if (product == null)
                {
                    return NotFound();
                }
                else
                    return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            var newProduct = new Product
            {
                MaSanPham = product.MaSanPham,
                GiaGiam = product.GiaGiam,
                GioiTinh = product.GioiTinh,
                GiaGoc = product.GiaGoc,
                MaDanhMuc = product.MaDanhMuc,
                MaThuongHieu = product.MaThuongHieu,
                MoTa = product.MoTa,
                TenSanPham = product.TenSanPham,
                XuatXu = product.XuatXu
            };
            products.Add(newProduct);
            return Ok(new
            {
                Success = true,
                Data = product
            });
        }

        [HttpPut("{MaSanPham}")]
        public IActionResult Edit(int MaSanPham, Product productEdit)
        {
            try
            {
                var product = products.SingleOrDefault(p => p.MaSanPham == MaSanPham);
                if (product == null)
                {
                    return NotFound();
                }
                else
                {
                    product.TenSanPham = productEdit.TenSanPham;
                    product.MoTa = productEdit.MoTa;
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{MaSanPham}")]
        public IActionResult Delete(int MaSanPham)
        {
            try
            {
                var product = products.SingleOrDefault(p => p.MaSanPham == MaSanPham);
                if (product == null)
                {
                    return NotFound();
                }
                else
                {
                    products.Remove(product);
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
