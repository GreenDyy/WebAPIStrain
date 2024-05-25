using Microsoft.EntityFrameworkCore;
using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;
using System.Collections.Generic;
using System.Linq;
using WebAPIStrain.Entities;

namespace WebAPIStrain.Services
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IrtContext dbContext;

        public OrderRepository(IrtContext context)
        {
            dbContext = context;
        }

        public List<OrderVM> GetAll()
        {
            var orders = dbContext.Orders.Select(o => new OrderVM
            {
                IdOrder = o.IdOrder,
                IdCustomer = o.IdCustomer,
                IdEmployee = o.IdEmployee,
                DateOrder = o.DateOrder,
                TotalPrice = o.TotalPrice,
                Status = o.Status,
                Note = o.Note
            }).ToList();
            return orders;
        }

        public OrderVM GetById(string id)
        {
            var order = dbContext.Orders.FirstOrDefault(o => o.IdOrder == int.Parse(id));
            if (order != null)
            {
                return new OrderVM
                {
                    IdOrder = order.IdOrder,
                    IdCustomer = order.IdCustomer,
                    IdEmployee = order.IdEmployee,
                    DateOrder = order.DateOrder,
                    TotalPrice = order.TotalPrice,
                    Status = order.Status,
                    Note = order.Note
                };
            }
            return null;
        }

        public OrderVM Create(OrderModel order)
        {
            var newOrder = new Order
            {
                IdCustomer = order.IdCustomer,
                IdEmployee = order.IdEmployee,
                DateOrder = order.DateOrder,
                TotalPrice = order.TotalPrice,
                Status = order.Status,
                Note = order.Note
            };
            dbContext.Add(newOrder);
            dbContext.SaveChanges();
            return new OrderVM
            {
                IdOrder = newOrder.IdOrder,
                IdCustomer = newOrder.IdCustomer,
                IdEmployee = newOrder.IdEmployee,
                DateOrder = newOrder.DateOrder,
                TotalPrice = newOrder.TotalPrice,
                Status = newOrder.Status,
                Note = newOrder.Note
            };
        }

        public bool Update(string id, OrderModel order)
        {
            var _order = dbContext.Orders.FirstOrDefault(o => o.IdOrder == int.Parse(id));
            if (_order != null)
            {
                _order.IdCustomer = order.IdCustomer;
                _order.IdEmployee = order.IdEmployee;
                _order.DateOrder = order.DateOrder;
                _order.TotalPrice = order.TotalPrice;
                _order.Status = order.Status;
                _order.Note = order.Note;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Delete(string id)
        {
            var order = dbContext.Orders.FirstOrDefault(o => o.IdOrder == int.Parse(id));
            if (order != null)
            {
                dbContext.Remove(order);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
