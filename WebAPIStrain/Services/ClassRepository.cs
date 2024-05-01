using WebAPIStrain.Entities;
using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;

namespace WebAPIStrain.Services
{
    public class ClassRepository : IClassRepository
    {
        private readonly IrtContext dbContext;

        public ClassRepository(IrtContext context)
        {
            dbContext = context;
        }

        public ClassVM Create(ClassModel inputClass)
        {
            var newClass = new Class
            {
                NameClass = inputClass.NameClass,
                IdPhylum = inputClass.IdPhylum,
            };
            dbContext.Add(newClass);
            dbContext.SaveChanges();
            return new ClassVM
            {
                IdClass = newClass.IdClass,
                NameClass = newClass.NameClass,
                IdPhylum = newClass.IdPhylum,
            };
        }

        public bool Delete(int id)
        {
            var cl = dbContext.Classes.FirstOrDefault(c => c.IdClass == id);
            if (cl != null)
            {
                dbContext.Classes.Remove(cl);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<ClassVM> GetAll()
        {
            var classes = dbContext.Classes.Select(c => new ClassVM
            {
                IdClass = c.IdClass,
                NameClass = c.NameClass,
                IdPhylum = c.IdPhylum,
            }).ToList();
            return classes;
        }

        public ClassVM GetById(int id)
        {
            var cl = dbContext.Classes.FirstOrDefault(c => c.IdClass == id);
            if (cl != null)
            {
                return new ClassVM
                {
                    IdClass = cl.IdClass,
                    NameClass = cl.NameClass,
                    IdPhylum = cl.IdPhylum,
                };
            }
            return null;
        }

        public bool Update(int id, ClassModel inputClass)
        {
            var cl = dbContext.Classes.FirstOrDefault(c => c.IdClass == id);
            if (cl != null)
            {
                cl.NameClass = inputClass.NameClass;
                cl.IdPhylum = inputClass.IdPhylum;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
