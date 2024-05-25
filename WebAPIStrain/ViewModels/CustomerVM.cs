using WebAPIStrain.Entities;
namespace WebAPIStrain.ViewModels
{
    public class CustomerVM
    {
        public string IdCustomer { get; set; } = null!;

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? FullName { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public string? Gender { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public byte[]? Image { get; set; }

        //account

        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? Status { get; set; }

        //cart

        public int IdCart { get; set; }

        public int? TotalProduct { get; set; }
    }
}
