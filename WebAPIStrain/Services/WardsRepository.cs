using Microsoft.EntityFrameworkCore;
using WebAPIStrain.Entities;
using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace WebAPIStrain.Services
{
    public class WardsRepository : IWardsRepository
    {
        private readonly IrtContext dbContext;

        public WardsRepository(IrtContext context)
        {
            dbContext = context;
        }

        public WardsVM Create(WardsModel wards)
        {
            var newWard = new Ward
            {
                Name = wards.Name,
                IdDistricts = wards.IdDistricts
            };
            dbContext.Add(newWard);
            dbContext.SaveChanges();
            return new WardsVM
            {
                IdWards = newWard.IdWards,
                Name = newWard.Name,
                IdDistricts = newWard.IdDistricts ?? 0
            };
        }

        public bool Delete(int id)
        {
            var ward = dbContext.Wards.FirstOrDefault(w => w.IdWards == id);
            if (ward != null)
            {
                dbContext.Remove(ward);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<WardsVM> GetAll()
        {
            var wards = dbContext.Wards.Select(w => new WardsVM
            {
                IdWards = w.IdWards,
                Name = w.Name,
                IdDistricts = w.IdDistricts ?? 0
            }).ToList();
            return wards;
        }

        public WardsVM GetById(int id)
        {
            var ward = dbContext.Wards.FirstOrDefault(w => w.IdWards == id);
            if (ward != null)
            {
                return new WardsVM
                {
                    IdWards = ward.IdWards,
                    Name = ward.Name,
                    IdDistricts = ward.IdDistricts ?? 0
                };
            }
            return null;
        }

        public bool Update(int id, WardsModel wards)
        {
            var _ward = dbContext.Wards.FirstOrDefault(w => w.IdWards == id);
            if (_ward != null)
            {
                _ward.Name = wards.Name;
                _ward.IdDistricts = wards.IdDistricts;

                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
