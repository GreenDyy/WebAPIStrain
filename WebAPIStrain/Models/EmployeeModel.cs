using WebAPIStrain.Entities;

namespace WebAPIStrain.Models
{
    public class EmployeeModel
    {
        //account
        public string? Username { get; set; }

        public string? Password { get; set; }
        public string? Status { get; set; }

        //employee
        public int? IdRole { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? IdCard { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public string? Gender { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Degree { get; set; }

        public string? Addresss { get; set; }

        public DateOnly? JoinDate { get; set; }

        public string? Institution { get; set; }

        public string? Department { get; set; }

        public string? Position { get; set; }

        public string? ResearchField { get; set; }

    }
}
