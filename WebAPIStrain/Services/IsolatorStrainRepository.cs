using Microsoft.EntityFrameworkCore;
using WebAPIStrain.Entities;
using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace WebAPIStrain.Services
{
    public class IsolatorStrainRepository : IIsolatorStrainRepository
    {
        private readonly IrtContext dbContext;

        public IsolatorStrainRepository(IrtContext context)
        {
            dbContext = context;
        }

        public IsolatorStrainVM Create(IsolatorStrainModel isolatorStrain)
        {
            var newIsolatorStrain = new IsolatorStrain
            {
                IdEmployee = isolatorStrain.ID_Employee,
                IdStrain = isolatorStrain.ID_Strain,
                YearOfIsolator = isolatorStrain.YearOfIsolator
            };
            dbContext.Add(newIsolatorStrain);
            dbContext.SaveChanges();
            return new IsolatorStrainVM
            {
                ID_Employee = newIsolatorStrain.IdEmployee,
                ID_Strain = newIsolatorStrain.IdStrain,
                YearOfIsolator = newIsolatorStrain.YearOfIsolator ?? 0,
            };
        }

        public bool Delete(string idEmployee, int idStrain)
        {
            var isolatorStrain = dbContext.IsolatorStrains.FirstOrDefault(p => p.IdEmployee == idEmployee && p.IdStrain == idStrain);
            if (isolatorStrain != null)
            {
                dbContext.Remove(isolatorStrain);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<IsolatorStrainVM> GetAll()
        {
            var isolatorStrains = dbContext.IsolatorStrains.Select(p => new IsolatorStrainVM
            {
                ID_Employee = p.IdEmployee,
                ID_Strain = p.IdStrain,
                YearOfIsolator = p.YearOfIsolator ?? 0
            }).ToList();
            return isolatorStrains;
        }

        public IsolatorStrainVM GetById(string idEmployee, int idStrain)
        {
            var isolatorStrain = dbContext.IsolatorStrains.FirstOrDefault(p => p.IdEmployee == idEmployee && p.IdStrain == idStrain);
            if (isolatorStrain != null)
            {
                return new IsolatorStrainVM
                {
                    ID_Employee = isolatorStrain.IdEmployee,
                    ID_Strain = isolatorStrain.IdStrain,
                    YearOfIsolator = isolatorStrain.YearOfIsolator ?? 0
                };
            }
            return null;
        }

        public bool Update(string idEmployee, int idStrain, IsolatorStrainModel isolatorStrain)
        {
            var _isolatorStrain = dbContext.IsolatorStrains.FirstOrDefault(p => p.IdEmployee == idEmployee && p.IdStrain == idStrain);
            if (_isolatorStrain != null)
            {
                _isolatorStrain.YearOfIsolator = isolatorStrain.YearOfIsolator;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}