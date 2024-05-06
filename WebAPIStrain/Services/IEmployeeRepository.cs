using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;

namespace WebAPIStrain.Services
{
    public interface IEmployeeRepository
    {
        public List<EmployeeVM> GetAll();
        public EmployeeVM GetById(string id);
        public EmployeeVM Create(EmployeeModel inputEmployee);
        public bool Update(string id, EmployeeModel inputEmployee);
        public bool Delete(string id);
    }
}
