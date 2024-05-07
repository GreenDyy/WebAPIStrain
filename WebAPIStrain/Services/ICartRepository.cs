using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;

namespace WebAPIStrain.Services
{
    public interface ICartRepository
    {
        public List<CartVM> GetAll();
        public CartVM GetById(int id);
        //public CartVM Create(CartModel inputCart);
        public bool Update(int id, CartModel inputCart);
        public bool Delete(int id);
    }
}
