﻿namespace WebAPIStrain.Models
{
    public class EmployeeModelWithOutPassword
    {
        //employee
        public int? IdRole { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? FullName { get; set; }

        public string? IdCard { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public string? Gender { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Degree { get; set; }

        public string? Address { get; set; }

        public DateOnly? JoinDate { get; set; }

        public byte[]? ImageEmployee { get; set; }

        public string? NameWard { get; set; }

        public string? NameDistrict { get; set; }

        public string? NameProvince { get; set; }

        // account

        public string? Username { get; set; }

        public string? Status { get; set; }
    }
}
