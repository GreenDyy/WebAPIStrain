using WebAPIStrain.Entities;
using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;

namespace WebAPIStrain.Services
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly IrtContext dbContext;

        public InventoryRepository(IrtContext context)
        {
            dbContext = context;
        }
        public InventoryVM Create(InventoryModel inputInventory)
        {
            var newInventory = new Inventory
            {
                IdStrain = inputInventory.IdStrain,
                Quantity = inputInventory.Quantity,
                Price = inputInventory.Price,
                EntryDate = inputInventory.EntryDate,
                Histories = inputInventory.Histories,
            };
            dbContext.Add(newInventory);
            dbContext.SaveChanges();
            return new InventoryVM
            {
                InventoryId = newInventory.InventoryId,
                IdStrain = newInventory.IdStrain,
                Quantity = newInventory.Quantity,
                Price = newInventory.Price,
                EntryDate = newInventory.EntryDate,
                Histories = newInventory.Histories,
            };
        }

        public bool Delete(int id)
        {
            var inventory = dbContext.Inventories.FirstOrDefault(p => p.InventoryId == id);
            if (inventory != null)
            {
                dbContext.Remove(inventory);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<InventoryVM> GetAll()
        {
            var inventories = dbContext.Inventories.Select(p => new InventoryVM
            {
                InventoryId = p.InventoryId,
                IdStrain = p.IdStrain,
                Price = p.Price,
                EntryDate = p.EntryDate,
                Quantity = p.Quantity,
                Histories = p.Histories,
                IdStrainNavigation = p.IdStrainNavigation
            }).ToList();
            return inventories;
        }

        public InventoryVM GetById(int id)
        {
            var inventory = dbContext.Inventories.FirstOrDefault(p => p.InventoryId == id);
            if (inventory != null)
            {
                return new InventoryVM
                {
                    InventoryId = inventory.InventoryId,
                    IdStrain = inventory.IdStrain,
                    Price = inventory.Price,
                    EntryDate = inventory.EntryDate,
                    Quantity = inventory.Quantity,
                    Histories = inventory.Histories,
                    IdStrainNavigation = inventory.IdStrainNavigation
                };
            }
            return null;
        }

        public InventoryVM GetByIdStrain(int id)
        {
            var inventory = dbContext.Inventories.FirstOrDefault(p => p.IdStrain == id);
            if (inventory != null)
            {
                return new InventoryVM
                {
                    InventoryId = inventory.InventoryId,
                    IdStrain = inventory.IdStrain,
                    Price = inventory.Price,
                    EntryDate = inventory.EntryDate,
                    Quantity = inventory.Quantity,
                    Histories = inventory.Histories,
                    IdStrainNavigation = inventory.IdStrainNavigation
                };
            }
            return null;
        }

        public bool Update(int id, InventoryModel inputInventory)
        {
            var _inventory = dbContext.Inventories.FirstOrDefault(p => p.InventoryId == id);
            if (_inventory != null)
            {
                _inventory.IdStrain = inputInventory.IdStrain;
                _inventory.Price = inputInventory.Price;
                _inventory.EntryDate = inputInventory.EntryDate;
                _inventory.Quantity = inputInventory.Quantity;
                _inventory.Histories = inputInventory.Histories;

                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateByIdStrain(int idStrain, InventoryModel inputInventory)
        {
            var _inventory = dbContext.Inventories.FirstOrDefault(p => p.IdStrain == idStrain);
            if (_inventory != null)
            {
                _inventory.IdStrain = inputInventory.IdStrain;
                _inventory.Price = inputInventory.Price;
                _inventory.EntryDate = inputInventory.EntryDate;
                _inventory.Quantity = inputInventory.Quantity;
                _inventory.Histories = inputInventory.Histories;

                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
