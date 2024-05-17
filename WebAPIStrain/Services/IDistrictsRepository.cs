using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;

namespace WebAPIStrain.Services
{
    public interface IDistrictsRepository
    {
        public List<DistrictsVM> GetAll();
        public DistrictsVM GetById(int id);
        public DistrictsVM Create(DistrictsModel inputDistricts);
        public bool Update(int id, DistrictsModel inputDistricts);
        public bool Delete(int id);
    }
}
