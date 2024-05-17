using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;
using System.Collections.Generic;

namespace WebAPIStrain.Services
{
    public interface IProvincesRepository
    {
        public List<ProvincesVM> GetAll();
        public ProvincesVM GetById(int id);
        public ProvincesVM Create(ProvincesModel inputProvinces);
        public bool Update(int id, ProvincesModel inputProvinces);
        public bool Delete(int id);
    }
}