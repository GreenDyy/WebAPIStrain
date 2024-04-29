using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;

namespace WebAPIStrain.Services
{
    public interface IPhylumRepository
    {
        public List<PhylumVM> GetAll();
        public PhylumVM GetById(int id);
        public PhylumVM Create(PhylumModel phylum);
        public bool Update(int id,PhylumModel phylum);
        public bool Delete(int id);
    }
}
