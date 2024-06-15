using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;
using System.Collections.Generic;

namespace WebAPIStrain.Services
{
    public interface IScienceNewspaperRepository
    {
        List<ScienceNewspaperVM> GetAll();
        ScienceNewspaperVM GetById(int id);
        ScienceNewspaperVM Create(ScienceNewspaperModel inputScienceNewspaper);
        bool Update(int id, ScienceNewspaperModel inputScienceNewspaper);
        bool Delete(int id);
        List<ScienceNewspaperVM> GetRandom();
    }
}
