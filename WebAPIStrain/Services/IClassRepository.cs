using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;

namespace WebAPIStrain.Services
{
    public interface IClassRepository
    {
        public List<ClassVM> GetAll();
        public ClassVM GetById(int id);
        public ClassVM Create(ClassModel inputClass);
        public bool Update(int id, ClassModel inputClass);
        public bool Delete(int id);
    }
}
