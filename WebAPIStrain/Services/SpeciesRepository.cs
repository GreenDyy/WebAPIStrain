using Microsoft.EntityFrameworkCore;
using WebAPIStrain.Entities;
using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;

namespace WebAPIStrain.Services
{
    public class SpeciesRepository : ISpeciesRepository
    {
        private readonly IrtContext dbContext;

        public SpeciesRepository(IrtContext context)
        {
            dbContext = context;
        }

        public SpeciesVM Create(SpeciesModel inputSpecies)
        {
            var newSpecies = new Species
            {
                NameSpecies = inputSpecies.NameSpecies,
                IdGenus = inputSpecies.IdGenus,
            };
            dbContext.Add(newSpecies);
            dbContext.SaveChanges();
            return new SpeciesVM
            {
                IdSpecies = newSpecies.IdSpecies,
                NameSpecies = newSpecies.NameSpecies,
                IdGenus = newSpecies.IdGenus
            };
        }

        public bool Delete(int id)
        {
            var species = dbContext.Species.FirstOrDefault(s => s.IdSpecies == id);
            if (species != null)
            {
                dbContext.Remove(species);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<SpeciesVM> GetAll()
        {
            var species = dbContext.Species.Select(s => new SpeciesVM
            {
                IdSpecies = s.IdSpecies,
                NameSpecies = s.NameSpecies,
                IdGenus = s.IdGenus
            }).ToList();
            return species;
        }

        public SpeciesVM GetById(int id)
        {
            var species = dbContext.Species.FirstOrDefault(s => s.IdSpecies == id);

            if (species != null)
            {
                return new SpeciesVM
                {
                    IdSpecies = species.IdSpecies,
                    NameSpecies = species.NameSpecies,
                    IdGenus = species.IdGenus
                };
            }
            return null;
        }

        public bool Update(int id, SpeciesModel inputSpecies)
        {
            var species = dbContext.Species.FirstOrDefault(s => s.IdSpecies == id);
            if (species != null)
            {
                species.NameSpecies = inputSpecies.NameSpecies;
                species.IdGenus = inputSpecies.IdGenus;

                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
