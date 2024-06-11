using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;

namespace WebAPIStrain.Services
{
    public interface IInventoryRepository
    {
        public List<InventoryVM> GetAll();
        public List<InventoryWithoutIdStrainNavigationVM> GetAllWithoutIdStrainNavigation();
        public InventoryVM GetById(int id);
        public InventoryVM GetByIdStrain(int idStrain);
        public InventoryVM Create(InventoryModel inputInventory);
        public bool Update(int id, InventoryModel inputInventory);
        public bool Delete(int id);

        public bool UpdateByIdStrain(int idStrain, InventoryModel inputInventory);
    }
}
