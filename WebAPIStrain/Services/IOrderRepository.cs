using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;

namespace WebAPIStrain.Services
{
    public interface IOrderRepository
    {
        List<OrderVM> GetAll();
        OrderVM GetById(string id);
        OrderVM Create(OrderModel inputProject);
        bool Update(string id, OrderModel inputProject);
        bool Delete(string id);
    }
}
