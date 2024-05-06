using WebAPIStrain.Entities;

namespace WebAPIStrain.ViewModels
{
    public class RoleForEmployeeVM
    {
        public int IdRole { get; set; }

        public string? RoleName { get; set; }

        public string? RoleDescription { get; set; }

        //public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
