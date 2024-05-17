using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;
using System.Collections.Generic;

namespace WebAPIStrain.Services
{
    public interface IWardsRepository
    {
        public List<WardsVM> GetAll();
        public WardsVM GetById(int id);
        public WardsVM Create(WardsModel inputWards);
        public bool Update(int id, WardsModel inputWards);
        public bool Delete(int id);
    }
}