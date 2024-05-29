using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebAPIStrain.Entities;
using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;

namespace WebAPIStrain.Services
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly IrtContext _dbContext;

        public OrderDetailRepository(IrtContext context)
        {
            _dbContext = context;
        }

        public List<OrderDetailVM> GetAll()
        {
            var orderDetails = _dbContext.OrderDetails.Select(od => new OrderDetailVM
            {
                IdOrderDetail = od.IdOrderDetail,
                IdOrder = od.IdOrder,
                IdStrain = od.IdStrain,
                Quantity = od.Quantity,
                Price = od.Price
            }).ToList();

            return orderDetails;
        }

        public OrderDetailVM GetById(int id)
        {
            var orderDetail = _dbContext.OrderDetails.FirstOrDefault(od => od.IdOrderDetail == id);
            if (orderDetail != null)
            {
                return new OrderDetailVM
                {
                    IdOrderDetail = orderDetail.IdOrderDetail,
                    IdOrder = orderDetail.IdOrder,
                    IdStrain = orderDetail.IdStrain,
                    Quantity = orderDetail.Quantity,
                    Price = orderDetail.Price
                };
            }
            return null;
        }

        public OrderDetailVM Create(OrderDetailModel inputOrderDetail)
        {
            var newOrderDetail = new OrderDetail
            {
                IdOrder = inputOrderDetail.IdOrder,
                IdStrain = inputOrderDetail.IdStrain,
                Quantity = inputOrderDetail.Quantity,
                Price = inputOrderDetail.Price
            };
            _dbContext.OrderDetails.Add(newOrderDetail);
            _dbContext.SaveChanges();
            return new OrderDetailVM
            {
                IdOrderDetail = newOrderDetail.IdOrderDetail,
                IdOrder = newOrderDetail.IdOrder,
                IdStrain = newOrderDetail.IdStrain,
                Quantity = newOrderDetail.Quantity,
                Price = newOrderDetail.Price
            };
        }

        public bool Update(int id, OrderDetailModel inputOrderDetail)
        {
            var orderDetail = _dbContext.OrderDetails.FirstOrDefault(od => od.IdOrderDetail == id);
            if (orderDetail != null)
            {
                orderDetail.IdOrder = inputOrderDetail.IdOrder;
                orderDetail.IdStrain = inputOrderDetail.IdStrain;
                orderDetail.Quantity = inputOrderDetail.Quantity;
                orderDetail.Price = inputOrderDetail.Price;
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            var orderDetail = _dbContext.OrderDetails.FirstOrDefault(od => od.IdOrderDetail == id);
            if (orderDetail != null)
            {
                _dbContext.OrderDetails.Remove(orderDetail);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<OrderDetailVM> GetAllByIdOrder(int idOrder)
        {
            var orderDetails = _dbContext.OrderDetails.Where(o => o.IdOrder == idOrder).Select(od => new OrderDetailVM
            {
                IdOrderDetail = od.IdOrderDetail,
                IdOrder = od.IdOrder,
                IdStrain = od.IdStrain,
                Quantity = od.Quantity,
                Price = od.Price,
                IdStrainNavigation = od.IdStrainNavigation,
            }).ToList();

            return orderDetails;
        }
    }
}
