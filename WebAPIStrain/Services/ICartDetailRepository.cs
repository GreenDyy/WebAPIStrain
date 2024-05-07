using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;

namespace WebAPIStrain.Services
{
    public interface ICartDetailRepository
    {
        public List<CartDetailVM> GetAll();
        public CartDetailVM GetById(int id);
        public CartDetailVM Create(CartDetailModel inputCartDetail);
        public bool Update(int id, CartDetailModel inputCartDetail);
        public bool Delete(int id);
    }
}
