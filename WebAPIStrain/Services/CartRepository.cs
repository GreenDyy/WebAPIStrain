using WebAPIStrain.Entities;
using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;

namespace WebAPIStrain.Services
{
    public class CartRepository : ICartRepository
    {
        private readonly IrtContext dbContext;

        public CartRepository(IrtContext context)
        {
            dbContext = context;
        }

        public List<CartVM> GetAll()
        {
            var carts = dbContext.Carts.Select(c => new CartVM
            {
                IdCart = c.IdCart,
                IdCustomer = c.IdCustomer,
                TotalProduct = c.TotalProduct
            }).ToList();
            return carts;
        }

        public CartVM GetById(int id)
        {
            var cart = dbContext.Carts.FirstOrDefault(c => c.IdCart == id);
            if(cart!=null)
            {
                return new CartVM
                {
                    IdCart = cart.IdCart,
                    IdCustomer = cart.IdCustomer,
                    TotalProduct = cart.TotalProduct
                };
            }
            return null;
        }
        public CartVM GetByIdCustomer(string idCustomer)
        {
            var cart = dbContext.Carts.FirstOrDefault(c => c.IdCustomer == idCustomer);
            if (cart != null)
            {
                return new CartVM
                {
                    IdCart = cart.IdCart,
                    IdCustomer = cart.IdCustomer,
                    TotalProduct = cart.TotalProduct,
                };
            }
            return null;
        }

        public bool Update(int id, CartModel inputCart)
        {
            var cart = dbContext.Carts.FirstOrDefault(c => c.IdCart == id);
            if(cart!=null )
            {
                cart.TotalProduct = inputCart.TotalProduct;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Delete(int id)
        {
            var cart = dbContext.Carts.FirstOrDefault(c => c.IdCart == id);
            if (cart != null)
            {
                dbContext.Carts.Remove(cart);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
