using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;

namespace WebAPIStrain.Services
{
    public interface IInventoryRepository
    {
        public List<InventoryVM> GetAll();
        public InventoryVM GetById(int id);
        public InventoryVM GetByIdStrain(int id);
        public InventoryVM Create(InventoryModel inputInventory);
        public bool Update(int id, InventoryModel inputInventory);
        public bool Delete(int id);
    }
}
