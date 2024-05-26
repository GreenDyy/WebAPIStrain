using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIStrain.Models;
using WebAPIStrain.Services;

namespace WebAPIStrain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
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