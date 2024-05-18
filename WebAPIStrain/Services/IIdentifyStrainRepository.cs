using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;
using System.Collections.Generic;

namespace WebAPIStrain.Services
{
    public interface IIdentifyStrainRepository
    {
        List<IdentifyStrainVM> GetAll();
        IdentifyStrainVM GetById(string idEmployee, int idStrain);
        IdentifyStrainVM Create(IdentifyStrainModel inputIdentifyStrain);
        bool Update(string idEmployee, int idStrain, IdentifyStrainModel inputIdentifyStrain);
        bool Delete(string idEmployee, int idStrain);
    }
}