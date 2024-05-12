using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;

namespace WebAPIStrain.Services
{
    public interface IConditionRepository
    {
        public List<ConditionalStrainVM> GetAll();
        public ConditionalStrainVM GetById(int id);
        public ConditionalStrainVM Create(ConditionalStrainModel inputCondition);
        public bool Update(int id, ConditionalStrainModel inputCondition);
        public bool Delete(int id);
    }
}
