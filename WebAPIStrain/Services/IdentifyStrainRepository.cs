using Microsoft.EntityFrameworkCore;
using WebAPIStrain.Entities;
using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace WebAPIStrain.Services
{
    public class IdentifyStrainRepository : IIdentifyStrainRepository
    {
        private readonly IrtContext dbContext;

        public IdentifyStrainRepository(IrtContext context)
        {
            dbContext = context;
        }

        public IdentifyStrainVM Create(IdentifyStrainModel identifyStrain)
        {
            var newIdentifyStrain = new IdentifyStrain
            {
                IdEmployee = identifyStrain.ID_Employee,
                IdStrain = identifyStrain.ID_Strain,
                YearOfIdentify = identifyStrain.Year_of_Identify
            };
            dbContext.Add(newIdentifyStrain);
            dbContext.SaveChanges();
            return new IdentifyStrainVM
            {
                ID_Employee = newIdentifyStrain.IdEmployee,
                ID_Strain = newIdentifyStrain.IdStrain,
                Year_of_Identify = newIdentifyStrain.YearOfIdentify ?? 0
            };
        }

        public bool Delete(string idEmployee, int idStrain)
        {
            var identifyStrain = dbContext.IdentifyStrains.FirstOrDefault(p => p.IdEmployee == idEmployee && p.IdStrain == idStrain);
            if (identifyStrain != null)
            {
                dbContext.Remove(identifyStrain);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<IdentifyStrainVM> GetAll()
        {
            var identifyStrains = dbContext.IdentifyStrains.Select(p => new IdentifyStrainVM
            {
                ID_Employee = p.IdEmployee,
                ID_Strain = p.IdStrain,
                Year_of_Identify = p.YearOfIdentify ?? 0
            }).ToList();
            return identifyStrains;
        }

        public IdentifyStrainVM GetById(string idEmployee, int idStrain)
        {
            var identifyStrain = dbContext.IdentifyStrains.FirstOrDefault(p => p.IdEmployee == idEmployee && p.IdStrain == idStrain);
            if (identifyStrain != null)
            {
                return new IdentifyStrainVM
                {
                    ID_Employee = identifyStrain.IdEmployee,
                    ID_Strain = identifyStrain.IdStrain,
                    Year_of_Identify = identifyStrain.YearOfIdentify ?? 0
                };
            }
            return null;
        }

        public bool Update(string idEmployee, int idStrain, IdentifyStrainModel identifyStrain)
        {
            var _identifyStrain = dbContext.IdentifyStrains.FirstOrDefault(p => p.IdEmployee == idEmployee && p.IdStrain == idStrain);
            if (_identifyStrain != null)
            {
                _identifyStrain.YearOfIdentify = identifyStrain.Year_of_Identify;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}