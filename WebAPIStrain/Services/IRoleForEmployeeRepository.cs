using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;

namespace WebAPIStrain.Services
{
    public interface IRoleForEmployeeRepository
    {
        public List<RoleForEmployeeVM> GetAll();
        public RoleForEmployeeVM GetById(int id);
        public RoleForEmployeeVM Create(RoleForEmployeeModel role);
        public bool Update(int id, RoleForEmployeeModel role);
        public bool Delete(int id);
    }
}
