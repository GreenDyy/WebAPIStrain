using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;
using System.Collections.Generic;

namespace WebAPIStrain.Services
{
    public interface IBillDetailRepository
    {
        List<BillDetailVM> GetAll();
        BillDetailVM GetById(int id);
        BillDetailVM Create(BillDetailModel inputBillDetail);
        bool Update(int id, BillDetailModel inputBillDetail);
        bool Delete(int id);
    }
}