using Microsoft.EntityFrameworkCore;
using WebAPIStrain.Entities;
using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace WebAPIStrain.Services
{
    public class DistrictsRepository : IDistrictsRepository
    {
        private readonly IrtContext dbContext;

        public DistrictsRepository(IrtContext context)
        {
            dbContext = context;
        }

        public DistrictsVM Create(DistrictsModel districts)
        {
            var newDistrict = new District
            {
                Name = districts.Name,
                IdProvinces = districts.IdProvinces
            };
            dbContext.Add(newDistrict);
            dbContext.SaveChanges();
            return new DistrictsVM
            {
                IdDistricts = newDistrict.IdDistricts,
                Name = newDistrict.Name,
                IdProvinces = newDistrict.IdProvinces ?? 0
            };
        }

        public bool Delete(int id)
        {
            var district = dbContext.Districts.FirstOrDefault(d => d.IdDistricts == id);
            if (district != null)
            {
                dbContext.Remove(district);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<DistrictsVM> GetAll()
        {
            var districts = dbContext.Districts.Select(d => new DistrictsVM
            {
                IdDistricts = d.IdDistricts,
                Name = d.Name,
                IdProvinces = d.IdProvinces ?? 0
            }).ToList();
            return districts;
        }

        public DistrictsVM GetById(int id)
        {
            var district = dbContext.Districts.FirstOrDefault(d => d.IdDistricts == id);
            if (district != null)
            {
                return new DistrictsVM
                {
                    IdDistricts = district.IdDistricts,
                    Name = district.Name,
                    IdProvinces = district.IdProvinces ?? 0
                };
            }
            return null;
        }

        public bool Update(int id, DistrictsModel districts)
        {
            var _district = dbContext.Districts.FirstOrDefault(d => d.IdDistricts == id);
            if (_district != null)
            {
                _district.Name = districts.Name;
                _district.IdProvinces = districts.IdProvinces;

                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
