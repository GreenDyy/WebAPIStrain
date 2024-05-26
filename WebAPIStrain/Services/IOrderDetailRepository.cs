using System.Collections.Generic;
using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;

namespace WebAPIStrain.Services
{
    public interface IOrderDetailRepository
    {
        List<OrderDetailVM> GetAll();
        OrderDetailVM GetById(int id);
        OrderDetailVM Create(OrderDetailModel inputOrderDetail);
        bool Update(int id, OrderDetailModel inputOrderDetail);
        bool Delete(int id);
        List<OrderDetailVM> GetAllByIdOrder(int idOrder);
    }
}