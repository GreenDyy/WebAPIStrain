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
        public EmployeeVM Login(Login login);
        public bool UpdatePass(string id, EmployeeModel inputEmployee);
        public bool UpdateDataNoPass(string id, EmployeeModel inputEmployee);
        public bool PatchPasswordEmployee(string id, string password);
        public List<EmployeeVM> GetAllEmployeeByIdProject(string idProject);
        public bool OpenAccount(string idEmployee);
        public bool LockAccount(string idEmployee);
        public bool ChangeRole(string idEmployee, int idRole);
    }
}
