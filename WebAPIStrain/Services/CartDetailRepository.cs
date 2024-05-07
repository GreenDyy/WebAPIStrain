//using WebAPIStrain.Entities;
//using WebAPIStrain.Models;
//using WebAPIStrain.ViewModels;

//namespace WebAPIStrain.Services
//{
//    public class CartDetailRepository : ICartDetailRepository
//    {
//        private readonly IrtContext dbContext;

//        public CartDetailRepository(IrtContext context)
//        {
//            dbContext = context;
//        }
//        public CartDetailVM Create(CartDetailModel inputCartDetail)
//        {
//            var newCartDetail = new Phylum
//            {
//                NamePhylum = phylum.NamePhylum,
//            };
//            dbContext.Add(newPhylum);
//            dbContext.SaveChanges();
//            return new PhylumVM
//            {
//                IdPhylum = newPhylum.IdPhylum,
//                NamePhylum = newPhylum.NamePhylum
//            };
//        }

//        public bool Delete(int id)
//        {
//            var phylum = dbContext.Phylums.FirstOrDefault(p => p.IdPhylum == id);
//            if (phylum != null)
//            {
//                dbContext.Remove(phylum);
//                dbContext.SaveChanges();
//                return true;
//            }
//            return false;
//        }

//        public List<CartDetailVM> GetAll()
//        {
//            var phylums = dbContext.Phylums.Select(p => new PhylumVM
//            {
//                IdPhylum = p.IdPhylum,
//                NamePhylum = p.NamePhylum
//            }).ToList();
//            return phylums;
//        }

//        public CartDetailVM GetById(int id)
//        {
//            var phylum = dbContext.Phylums.FirstOrDefault(p => p.IdPhylum == id);
//            if (phylum != null)
//            {
//                return new PhylumVM
//                {
//                    IdPhylum = phylum.IdPhylum,
//                    NamePhylum = phylum.NamePhylum
//                };
//            }
//            return null;
//        }

//        public bool Update(int id, CartDetailModel inputCartDetail)
//        {
//            var _phylum = dbContext.Phylums.FirstOrDefault(p => p.IdPhylum == id);
//            if (_phylum != null)
//            {
//                _phylum.NamePhylum = phylum.NamePhylum;

//                dbContext.SaveChanges();
//                return true;
//            }
//            return false;
//        }
//    }
//}
