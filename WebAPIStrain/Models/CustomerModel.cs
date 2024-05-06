using WebAPIStrain.Entities;

namespace WebAPIStrain.Models
{
    public class CustomerModel
    {
        //profile

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? FullName { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public string? Gender { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        //account


        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? Status { get; set; }
    }
}
