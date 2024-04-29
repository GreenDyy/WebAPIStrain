using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;

namespace WebAPIStrain.Services
{
    public interface ICustomerRepository
    {
        public List<CustomerVM> GetAll();
        public CustomerVM GetById(int id);
        public CustomerVM Create(CustomerModel customer);
        public bool Update(int id, CustomerModel customer);
        public bool Delete(int id);
    }
}
