using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;
using System.Collections.Generic;

namespace WebAPIStrain.Services
{
    public interface IBillRepository
    {
        List<BillVM> GetAll();
        BillVM GetById(string id);
        BillVM Create(BillModel inputBill);
        bool Update(string id, BillModel inputBill);
        bool Delete(string id);
    }
}