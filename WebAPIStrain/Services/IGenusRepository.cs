using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;

namespace WebAPIStrain.Services
{
    public interface IGenusRepository
    {
        public List<GenusVM> GetAll();
        public GenusVM GetById(int id);
        public GenusVM Create(GenusModel inputGenus);
        public bool Update(int id, GenusModel inputGenus);
        public bool Delete(int id);
    }
}
