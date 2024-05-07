using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;

namespace WebAPIStrain.Services
{
    public interface IPhylumRepository
    {
        public List<PhylumVM> GetAll();
        public PhylumVM GetById(int id);
        public PhylumVM Create(PhylumModel inputPhylum);
        public bool Update(int id,PhylumModel inputPhylum);
        public bool Delete(int id);
    }
}
