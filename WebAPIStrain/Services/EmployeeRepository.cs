using Microsoft.AspNetCore.Rewrite;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using WebAPIStrain.Entities;
using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAPIStrain.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IrtContext dbContext;

        public EmployeeRepository(IrtContext context)
        {
            dbContext = context;
        }

        public EmployeeVM Create(EmployeeModel inputEmployee)
        {
            string newIdEmployee;
            string? lastIdEmployee;
            var lastEmployee = dbContext.Employees.OrderBy(e => e.IdEmployee).LastOrDefault();
            if (lastEmployee != null)
            {
                lastIdEmployee = lastEmployee.IdEmployee;
                string partNumberId = lastIdEmployee.Substring(2); //0001
                int number = int.Parse(partNumberId);
                number++;
                partNumberId = number.ToString("D3");
                newIdEmployee = "NV" + partNumberId;
            }
            else
            {
                newIdEmployee = "NV0001";
            }

            //employee
            var employee = new Employee
            {
                IdEmployee = newIdEmployee,
                IdRole = inputEmployee.IdRole,
                FirstName = inputEmployee.FirstName,
                LastName = inputEmployee.LastName,
                FullName = inputEmployee.LastName + " " + inputEmployee.FirstName,
                IdCard = inputEmployee.IdCard,
                DateOfBirth = inputEmployee.DateOfBirth,
                Gender = inputEmployee.Gender,
                Email = inputEmployee.Email,
                PhoneNumber = inputEmployee.PhoneNumber,
                Degree = inputEmployee.Degree,
                Address = inputEmployee.Address,
                JoinDate = inputEmployee.JoinDate,
                ImageEmployee = inputEmployee.ImageEmployee,
                NameWard = inputEmployee.NameWard,
                NameDistrict = inputEmployee.NameDistrict,
                NameProvince = inputEmployee.NameProvince,
            };
            dbContext.Employees.Add(employee);
            dbContext.SaveChanges();

            //accout
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(inputEmployee.Password);


            var account = new AccountForEmployee
            {
                IdEmployee = employee.IdEmployee,
                Username = inputEmployee.Username,
                Password = hashedPassword,
                Status = inputEmployee.Status,
            };
            dbContext.AccountForEmployees.Add(account);
            dbContext.SaveChanges();

            return new EmployeeVM
            {
                IdEmployee = employee.IdEmployee,
                IdRole = employee.IdRole,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                FullName = employee.FullName,
                IdCard = employee.IdCard,
                DateOfBirth = employee.DateOfBirth,
                Gender = employee.Gender,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Degree = employee.Degree,
                Address = employee.Address,
                JoinDate = employee.JoinDate,
                ImageEmployee = employee.ImageEmployee,
                NameWard = employee.NameWard,
                NameDistrict = employee.NameDistrict,
                NameProvince = employee.NameProvince,

                Username = account.Username,
                Password = account.Password,
                Status = account.Status
            };
        }

        public bool Delete(string id)
        {
            var employee = dbContext.Employees.Include(e => e.AccountForEmployee).FirstOrDefault(e => e.IdEmployee == id);
            if (employee != null)
            {
                dbContext.AccountForEmployees.Remove(employee.AccountForEmployee);
                dbContext.Remove(employee);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<EmployeeVM> GetAll()
        {
            //truy xuất từ accoutn trước, do thằng employee cầm key của account
            var employees = dbContext.Employees.Include(employee => employee.AccountForEmployee).Select(employee => new EmployeeVM
            {
                IdEmployee = employee.IdEmployee,
                IdRole = employee.IdRole,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                FullName = employee.FullName,
                IdCard = employee.IdCard,
                DateOfBirth = employee.DateOfBirth,
                Gender = employee.Gender,
                Email = employee.   Email,
                PhoneNumber = employee.PhoneNumber,
                Degree = employee.Degree,
                Address = employee.Address,
                JoinDate = employee.JoinDate,
                ImageEmployee = employee.ImageEmployee,
                NameWard = employee.NameWard,
                NameDistrict = employee.NameDistrict,
                NameProvince = employee.NameProvince,

                Username = employee.AccountForEmployee.Username,
                Password = employee.AccountForEmployee.Password,
                Status = employee.AccountForEmployee.Status,
            }).ToList();
            return employees;
        }

        public EmployeeVM GetById(string id)
        {
            var employee = dbContext.Employees.Include(e => e.AccountForEmployee).FirstOrDefault(e => e.IdEmployee == id);
            if (employee != null)
            {
                return new EmployeeVM
                {
                    IdEmployee = employee.IdEmployee,
                    IdRole = employee.IdRole,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    FullName = employee.FullName,
                    IdCard = employee.IdCard,
                    DateOfBirth = employee.DateOfBirth,
                    Gender = employee.Gender,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    Degree = employee.Degree,
                    Address = employee.Address,
                    JoinDate = employee.JoinDate,
                    ImageEmployee = employee.ImageEmployee,
                    NameWard = employee.NameWard,
                    NameDistrict = employee.NameDistrict,
                    NameProvince = employee.NameProvince,

                    Username = employee.AccountForEmployee.Username,
                    Password = employee.AccountForEmployee.Password,
                    Status = employee.AccountForEmployee.Status,
                };
            }
            return null;
        }

        public bool Update(string id, EmployeeModelWithOutPassword inputEmployee)
        {
            var employee = dbContext.Employees.Include(e => e.AccountForEmployee).FirstOrDefault(e => e.IdEmployee == id);
            if (employee != null)
            {
                employee.IdRole = inputEmployee.IdRole;
                employee.FirstName = inputEmployee.FirstName;
                employee.LastName = inputEmployee.LastName;
                employee.FullName = $"{inputEmployee.LastName} {inputEmployee.FirstName}";
                employee.IdCard = inputEmployee.IdCard;
                employee.DateOfBirth = inputEmployee.DateOfBirth;
                employee.Gender = inputEmployee.Gender;
                employee.Email = inputEmployee.Email;
                employee.PhoneNumber = inputEmployee.PhoneNumber;
                employee.Degree = inputEmployee.Degree;
                employee.Address = inputEmployee.Address;
                employee.JoinDate = inputEmployee.JoinDate;
                employee.ImageEmployee = inputEmployee.ImageEmployee;
                employee.NameWard = inputEmployee.NameWard;
                employee.NameDistrict = inputEmployee.NameDistrict;
                employee.NameProvince = inputEmployee.NameProvince;

                employee.AccountForEmployee.Username = inputEmployee.Username;
                employee.AccountForEmployee.Status = inputEmployee.Status;

                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public EmployeeVM Login(Login login)
        {
            var account = dbContext.AccountForEmployees.FirstOrDefault(ac => ac.Username == login.Username);
            if (account != null)
            {
                bool isPasswordMatch = BCrypt.Net.BCrypt.Verify(login.Password, account.Password);
                if (isPasswordMatch)
                {
                    var profile = dbContext.Employees.FirstOrDefault(c => c.IdEmployee == account.IdEmployee);
                    return new EmployeeVM
                    {
                        IdEmployee = profile.IdEmployee,
                        IdRole = profile.IdRole,
                        FirstName = profile.FirstName,
                        LastName = profile.LastName,
                        FullName = profile.FullName,
                        IdCard = profile.IdCard,
                        DateOfBirth = profile.DateOfBirth,
                        Gender = profile.Gender,
                        Email = profile.Email,
                        PhoneNumber = profile.PhoneNumber,
                        Degree = profile.Degree,
                        Address = profile.Address,
                        JoinDate = profile.JoinDate,
                        ImageEmployee = profile.ImageEmployee,
                        NameWard = profile.NameWard,
                        NameDistrict = profile.NameDistrict,
                        NameProvince = profile.NameProvince,

                        Username = account.Username,
                        Password = account.Password,
                        Status =account.Status,
                    };
                }
                return null;
            }
            return null;
        }
    }
}
