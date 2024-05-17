using Microsoft.EntityFrameworkCore;
using WebAPIStrain.Entities;
using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace WebAPIStrain.Services
{
    public class ProvincesRepository : IProvincesRepository
    {
        private readonly IrtContext dbContext;

        public ProvincesRepository(IrtContext context)
        {
            dbContext = context;
        }

        public ProvincesVM Create(ProvincesModel provinces)
        {
            var newProvince = new Province
            {
                Name = provinces.Name,
            };
            dbContext.Add(newProvince);
            dbContext.SaveChanges();
            return new ProvincesVM
            {
                IdProvinces = newProvince.IdProvinces,
                Name = newProvince.Name
            };
        }

        public bool Delete(int id)
        {
            var province = dbContext.Provinces.FirstOrDefault(p => p.IdProvinces == id);
            if (province != null)
            {
                dbContext.Remove(province);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<ProvincesVM> GetAll()
        {
            var provinces = dbContext.Provinces.Select(p => new ProvincesVM
            {
                IdProvinces = p.IdProvinces,
                Name = p.Name
            }).ToList();
            return provinces;
        }

        public ProvincesVM GetById(int id)
        {
            var province = dbContext.Provinces.FirstOrDefault(p => p.IdProvinces == id);
            if (province != null)
            {
                return new ProvincesVM
                {
                    IdProvinces = province.IdProvinces,
                    Name = province.Name
                };
            }
            return null;
        }

        public bool Update(int id, ProvincesModel provinces)
        {
            var _province = dbContext.Provinces.FirstOrDefault(p => p.IdProvinces == id);
            if (_province != null)
            {
                _province.Name = provinces.Name;

                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}