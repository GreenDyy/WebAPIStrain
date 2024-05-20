using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;

namespace WebAPIStrain.Services
{
    public interface IStrainRepository
    {
        public List<StrainVM> GetAll();
        public List<StrainVM> GetAll(string? search, string? sortBy, int page);
        public StrainVM GetById(int id);
        public StrainVM GetByStrainNumber(string strainNumber);
        public StrainVM Create(StrainModel strain);
        public bool Update(int id, StrainModel strain);
        public bool UpdateStrainNumber(int id, string strainNumber);
        public bool Delete(int id);
    }
}
