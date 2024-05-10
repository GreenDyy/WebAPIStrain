using WebAPIStrain.Entities;
using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;

namespace WebAPIStrain.Services
{
    public class CartDetailRepository : ICartDetailRepository
    {
        private readonly IrtContext dbContext;

        public CartDetailRepository(IrtContext context)
        {
            dbContext = context;
        }
        public CartDetailVM Create(CartDetailModel inputCartDetail)
        {
            var newCartDetail = new CartDetail
            {
                IdCart = inputCartDetail.IdCart,
                IdStrain = inputCartDetail.IdStrain,
                QuantityOfStrain = inputCartDetail.QuantityOfStrain,
            };
            dbContext.Add(newCartDetail);
            dbContext.SaveChanges();
            return new CartDetailVM
            {
                IdCartDetail = newCartDetail.IdCartDetail,
                IdCart = newCartDetail.IdCart,
                IdStrain = newCartDetail.IdStrain,
                QuantityOfStrain = newCartDetail.QuantityOfStrain,
            };
        }

        public bool Delete(int id)
        {
            var cartDetail = dbContext.CartDetails.FirstOrDefault(cartDetail => cartDetail.IdCartDetail == id);
            if (cartDetail != null)
            {
                dbContext.Remove(cartDetail);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<CartDetailVM> GetAll()
        {
            var cartDetails = dbContext.CartDetails.Select(cartDetail => new CartDetailVM
            {
                IdCartDetail = cartDetail.IdCartDetail,
                IdCart = cartDetail.IdCart,
                IdStrain = cartDetail.IdStrain,
                QuantityOfStrain = cartDetail.QuantityOfStrain,
                IdCartNavigation = cartDetail.IdCartNavigation,
                IdStrainNavigation = cartDetail.IdStrainNavigation,
            }).ToList();
            return cartDetails;
        }

        public CartDetailVM GetById(int id)
        {
            var cartDetail = dbContext.CartDetails.FirstOrDefault(cd => cd.IdCartDetail == id);
            if (cartDetail != null)
            {
                return new CartDetailVM
                {
                    IdCartDetail = cartDetail.IdCartDetail,
                    IdCart = cartDetail.IdCart,
                    IdStrain = cartDetail.IdStrain,
                    QuantityOfStrain = cartDetail.QuantityOfStrain,
                    IdStrainNavigation= cartDetail.IdStrainNavigation,
                    IdCartNavigation = cartDetail.IdCartNavigation
                };
            }
            return null;
        }

        public bool Update(int id, CartDetailModel inputCartDetail)
        {
            var cartDetail= dbContext.CartDetails.FirstOrDefault(cd => cd.IdCartDetail == id);
            if (cartDetail!= null)
            {
                cartDetail.IdStrain = inputCartDetail.IdStrain;
                cartDetail.QuantityOfStrain = inputCartDetail.QuantityOfStrain;

                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
