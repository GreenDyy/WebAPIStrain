using Microsoft.EntityFrameworkCore;
using WebAPIStrain.Entities;
using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;

namespace WebAPIStrain.Services
{
    public class PhylumRepository : IPhylumRepository
    {
        private readonly IrtContext dbContext;

        public PhylumRepository(IrtContext context)
        {
            dbContext = context;
        }
        public PhylumVM Create(PhylumModel phylum)
        {
            var newPhylum = new Phylum
            {
                NamePhylum = phylum.NamePhylum,
            };
            dbContext.Add(newPhylum);
            dbContext.SaveChanges();
            return new PhylumVM
            {
                IdPhylum = newPhylum.IdPhylum,
                NamePhylum = newPhylum.NamePhylum
            };
        }

        public bool Delete(int id)
        {
            var phylum = dbContext.Phylums.FirstOrDefault(p => p.IdPhylum == id);
            if (phylum != null)
            {
                dbContext.Remove(phylum);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<PhylumVM> GetAll()
        {
            var phylums = dbContext.Phylums.Select(p => new PhylumVM
            {
                IdPhylum = p.IdPhylum,
                NamePhylum = p.NamePhylum,
                Classes = p.Classes.Select(c => new ClassVM
                {
                    IdClass = c.IdClass,
                    NameClass = c.NameClass,
                    IdPhylum = c.IdPhylum,
                    Genus = c.Genus.Select(g => new GenusVM
                    {
                        IdGenus = g.IdGenus,
                        NameGenus = g.NameGenus,
                        IdClass = g.IdClass,
                        Species = g.Species.Select(s => new SpeciesVM
                        {
                            IdSpecies = s.IdSpecies,
                            NameSpecies = s.NameSpecies,
                            IdGenus = s.IdGenus,
                        }).ToList()
                    }).ToList()
                }).ToList()
            }).ToList();

            return phylums;
        }

        public PhylumVM GetById(int id)
        {
            var phylum = dbContext.Phylums.FirstOrDefault(p => p.IdPhylum == id);
            if (phylum != null)
            {
                return new PhylumVM
                {
                    IdPhylum = phylum.IdPhylum,
                    NamePhylum = phylum.NamePhylum
                };
            }
            return null;
        }

        public bool Update(int id, PhylumModel phylum)
        {
            var _phylum = dbContext.Phylums.FirstOrDefault(p => p.IdPhylum == id);
            if (_phylum != null)
            {
                _phylum.NamePhylum = phylum.NamePhylum;

                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
