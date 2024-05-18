using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;
using System.Collections.Generic;

namespace WebAPIStrain.Services
{
    public interface IPartnerRepository
    {
        List<PartnerVM> GetAll();
        PartnerVM GetById(int id);
        PartnerVM Create(PartnerModel inputPartner);
        bool Update(int id, PartnerModel inputPartner);
        bool Delete(int id);
    }
}
