using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;
using System.Collections.Generic;

namespace WebAPIStrain.Services
{
    public interface IAccountForCustomerRepository
    {
        List<AccountForCustomerVM> GetAll();
        AccountForCustomerVM GetById(string id);
        AccountForCustomerVM Create(AccountForCustomerModel inputAccount);
        bool Update(string id, AccountForCustomerModel inputAccount);
        bool Delete(string id);
    }
}
