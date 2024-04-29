using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;

namespace WebAPIStrain.Services
{
    public interface IEmployeeRepository
    {
        public List<EmployeeVM> GetAll();
        public EmployeeVM GetById(int id);
        public EmployeeVM Create(EmployeeModel inputEmployee);
        public bool Update(int id, EmployeeModel inputEmployee);
        public bool Delete(int id);
    }
}
