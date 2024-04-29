using WebAPIStrain.Entities;
namespace WebAPIStrain.ViewModels
{
    public class CustomerVM
    {
        public int Id { get; set; }

        public string? IdCustomer { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? FullName { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public string? Gender { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        //public virtual ICollection<AccountForCustomer> AccountForCustomers { get; set; } = new List<AccountForCustomer>();

        //public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

        //public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
        //account
        public int IdAccountForCustomer { get; set; }

        //public int? Id { get; set; } trên có rồi

        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? Status { get; set; }
    }
}
