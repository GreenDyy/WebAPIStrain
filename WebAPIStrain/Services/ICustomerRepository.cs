using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;

namespace WebAPIStrain.Services
{
    public interface ICustomerRepository
    {
        public List<CustomerVM> GetAll();
        public CustomerVM GetById(string id);
        public CustomerVM Create(CustomerModel customer);
        public bool Update(string id, CustomerModel customer);
        public bool Delete(string id);
        public CustomerVM Login(string username, string password);
    }
}
