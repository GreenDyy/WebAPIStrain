using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;

namespace WebAPIStrain.Services
{
    public interface ISpeciesRepository
    {
        public List<SpeciesVM> GetAll();
        public SpeciesVM GetById(int id);
        public SpeciesVM Create(SpeciesModel inputSpecies);
        public bool Update(int id, SpeciesModel inputSpecies);
        public bool Delete(int id);
    }
}
