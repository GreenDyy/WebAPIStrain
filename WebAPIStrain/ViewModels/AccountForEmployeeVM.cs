using WebAPIStrain.Entities;

namespace WebAPIStrain.ViewModels
{
    public class AccountForEmployeeVM
    {
        public int IdAccount { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? Status { get; set; }

        //public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
