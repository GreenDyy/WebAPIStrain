using WebAPIStrain.Entities;
using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;

namespace WebAPIStrain.Services
{
    public class RoleForEmployeeRepository : IRoleForEmployeeRepository
    {
        private readonly IrtContext dbContext;

        public RoleForEmployeeRepository(IrtContext context)
        {
            dbContext = context;
        }
        public RoleForEmployeeVM Create(RoleForEmployeeModel inputRole)
        {
            var newRole = new RoleForEmployee
            {
                RoleName = inputRole.RoleName,
                RoleDescription = inputRole.RoleDescription,
            };
            dbContext.Add(newRole);
            dbContext.SaveChanges();
            return new RoleForEmployeeVM
            {
                IdRole = newRole.IdRole,
                RoleName = newRole.RoleName,
                RoleDescription = newRole.RoleDescription,
            };
        }

        public bool Delete(int id)
        {
            var role = dbContext.RoleForEmployees.FirstOrDefault(role => role.IdRole == id);
            if (role != null)
            {
                dbContext.Remove(role);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<RoleForEmployeeVM> GetAll()
        {
            var roles = dbContext.RoleForEmployees.Select(role => new RoleForEmployeeVM
            {
                IdRole = role.IdRole,
                RoleName = role.RoleName,
                RoleDescription = role.RoleDescription
            }).ToList();
            return roles;
        }

        public RoleForEmployeeVM GetById(int id)
        {
            var role = dbContext.RoleForEmployees.FirstOrDefault(role => role.IdRole == id);
            if (role != null)
            {
                return new RoleForEmployeeVM
                {
                    IdRole = role.IdRole,
                    RoleName = role.RoleName,
                    RoleDescription = role.RoleDescription
                };
            }
            return null;
        }

        public bool Update(int id, RoleForEmployeeModel inputRole)
        {
            var role = dbContext.RoleForEmployees.FirstOrDefault(role => role.IdRole == id);
            if (role != null)
            {
                role.RoleName = inputRole.RoleName;
                role.RoleDescription = inputRole.RoleDescription;

                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
