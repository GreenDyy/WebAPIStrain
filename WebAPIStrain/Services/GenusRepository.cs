using WebAPIStrain.Entities;
using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;

namespace WebAPIStrain.Services
{
    public class GenusRepository : IGenusRepository
    {
        private readonly IrtContext dbContext;

        public GenusRepository(IrtContext context)
        {
            dbContext = context;
        }

        public GenusVM Create(GenusModel inputGenus)
        {
            var newGenus = new Genu
            {
                NameGenus = inputGenus.NameGenus,
                IdClass = inputGenus.IdClass,
            };
            dbContext.Add(newGenus);
            dbContext.SaveChanges();
            return new GenusVM
            {
                IdGenus = newGenus.IdGenus,
                NameGenus = newGenus.NameGenus,
                IdClass = newGenus.IdClass
            };
        }

        public bool Delete(int id)
        {
            var cl = dbContext.Genus.FirstOrDefault(c => c.IdGenus == id);
            if (cl != null)
            {
                dbContext.Genus.Remove(cl);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<GenusVM> GetAll()
        {
            var classes = dbContext.Genus.Select(c => new GenusVM
            {
                IdGenus = c.IdGenus,
                NameGenus = c.NameGenus,
                IdClass = c.IdClass
            }).ToList();
            return classes;
        }

        public GenusVM GetById(int id)
        {
            var cl = dbContext.Genus.FirstOrDefault(c => c.IdGenus == id);
            if (cl != null)
            {
                return new GenusVM
                {
                    IdGenus = cl.IdGenus,
                    NameGenus = cl.NameGenus,
                    IdClass = cl.IdClass
                };
            }
            return null;
        }

        public bool Update(int id, GenusModel inputGenus)
        {
            var cl = dbContext.Genus.FirstOrDefault(c => c.IdGenus == id);
            if (cl != null)
            {
                cl.NameGenus = inputGenus.NameGenus;
                cl.IdClass = inputGenus.IdClass;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
