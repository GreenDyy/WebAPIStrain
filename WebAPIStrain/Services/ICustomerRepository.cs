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
        public bool ChangePass(string id, CustomerModel customer);
        public bool UpdateDataNoPass(string id, CustomerModel customer);
        public bool Delete(string id);
        public CustomerVM Login(Login login);
        public bool ResetPassword(string email, string newPass);
        public bool CheckExistEmail(string email);
        public bool CheckExistUserName(string userName);
        public bool CheckExistEmailWithoutSelf(string email, string idCustomer);
        public CustomerVM LoginWithGoogle(string email);

    }
}
