using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;
using System.Collections.Generic;

namespace WebAPIStrain.Services
{
    public interface IIsolatorStrainRepository
    {
        List<IsolatorStrainVM> GetAll();
        IsolatorStrainVM GetById(string idEmployee, int idStrain);
        IsolatorStrainVM Create(IsolatorStrainModel inputIsolatorStrain);
        bool Update(string idEmployee, int idStrain, IsolatorStrainModel inputIsolatorStrain);
        bool Delete(string idEmployee, int idStrain);
    }
}