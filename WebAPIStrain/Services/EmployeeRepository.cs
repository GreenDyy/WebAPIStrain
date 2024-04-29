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
                partNumberId = number.ToString("D4");
                newIdEmployee = "NV" + partNumberId;
            }
            else
            {
                newIdEmployee = "NV0001";
            }

            //NV thì tạo account trước rồi tới infor, thằng nào làm db lỏ vc

            //accout
            var account = new AccountForEmployee
            {
                Username = inputEmployee.Username,
                Password = inputEmployee.Password,
                Status = inputEmployee.Status,
            };
            dbContext.AccountForEmployees.Add(account);
            dbContext.SaveChanges();
            //employee
            var employee = new Employee
            {
                IdEmployee = newIdEmployee,
                IdRole = inputEmployee.IdRole,
                IdAccount = account.IdAccount, // lấy trên xuống
                FirstName = inputEmployee.FirstName,
                LastName = inputEmployee.LastName,
                FullName = inputEmployee.LastName + " " + inputEmployee.FirstName,
                IdCard = inputEmployee.IdCard,
                DateOfBirth = inputEmployee.DateOfBirth,
                Gender = inputEmployee.Gender,
                Email = inputEmployee.Email,
                PhoneNumber = inputEmployee.PhoneNumber,
                Degree = inputEmployee.Degree,
                Addresss = inputEmployee.Addresss,
                JoinDate = inputEmployee.JoinDate,
                Institution = inputEmployee.Institution,
                Department = inputEmployee.Department,
                Position = inputEmployee.Position,
                ResearchField = inputEmployee.ResearchField,
            };
            dbContext.Employees.Add(employee);
            dbContext.SaveChanges();
            return new EmployeeVM
            {
                IdEmployee = employee.IdEmployee,
                IdAccount = account.IdAccount,
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
                Addresss = employee.Addresss,
                JoinDate = employee.JoinDate,
                Institution = employee.Institution,
                Department = employee.Department,
                Position = employee.Position,
                ResearchField = employee.ResearchField,

                Username = account.Username,
                Password = account.Password,
                Status = account.Status
            };
        }

        public bool Delete(int id)
        {
            var employee = dbContext.AccountForEmployees.Include(e => e.Employees).FirstOrDefault(e => e.IdAccount == id);
            if (employee != null)
            {
                dbContext.RemoveRange(employee.Employees);
                dbContext.Remove(employee);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<EmployeeVM> GetAll()
        {
            //truy xuất từ accoutn trước, do thằng employee cầm key của account
            var employees = dbContext.AccountForEmployees.Include(employee => employee.Employees).Select(employee => new EmployeeVM
            {
                IdAccount = employee.IdAccount,
                Username = employee.Username,
                Password = employee.Password,
                Status = employee.Status,

                IdEmployee = employee.Employees.FirstOrDefault().IdEmployee,
                IdRole = employee.Employees.FirstOrDefault().IdRole,
                FirstName = employee.Employees.FirstOrDefault().FirstName,
                LastName = employee.Employees.FirstOrDefault().LastName,
                FullName = employee.Employees.FirstOrDefault().FullName,
                IdCard = employee.Employees.FirstOrDefault().IdCard,
                DateOfBirth = employee.Employees.FirstOrDefault().DateOfBirth,
                Gender = employee.Employees.FirstOrDefault().Gender,
                Email = employee.Employees.FirstOrDefault().Email,
                PhoneNumber = employee.Employees.FirstOrDefault().PhoneNumber,
                Degree = employee.Employees.FirstOrDefault().Degree,
                Addresss = employee.Employees.FirstOrDefault().Addresss,
                JoinDate = employee.Employees.FirstOrDefault().JoinDate,
                Institution = employee.Employees.FirstOrDefault().Institution,
                Department = employee.Employees.FirstOrDefault().Department,
                Position = employee.Employees.FirstOrDefault().Position,
                ResearchField = employee.Employees.FirstOrDefault().ResearchField,
            }).ToList();
            return employees;
        }

        public EmployeeVM GetById(int id)
        {
            var employee = dbContext.AccountForEmployees.Include(e => e.Employees).FirstOrDefault(e => e.IdAccount == id);
            if (employee != null)
            {
                return new EmployeeVM
                {
                    IdAccount = employee.IdAccount,
                    Username = employee.Username,
                    Password = employee.Password,
                    Status = employee.Status,

                    IdEmployee = employee.Employees.FirstOrDefault().IdEmployee,
                    IdRole = employee.Employees.FirstOrDefault().IdRole,
                    FirstName = employee.Employees.FirstOrDefault().FirstName,
                    LastName = employee.Employees.FirstOrDefault().LastName,
                    FullName = employee.Employees.FirstOrDefault().FullName,
                    IdCard = employee.Employees.FirstOrDefault().IdCard,
                    DateOfBirth = employee.Employees.FirstOrDefault().DateOfBirth,
                    Gender = employee.Employees.FirstOrDefault().Gender,
                    Email = employee.Employees.FirstOrDefault().Email,
                    PhoneNumber = employee.Employees.FirstOrDefault().PhoneNumber,
                    Degree = employee.Employees.FirstOrDefault().Degree,
                    Addresss = employee.Employees.FirstOrDefault().Addresss,
                    JoinDate = employee.Employees.FirstOrDefault().JoinDate,
                    Institution = employee.Employees.FirstOrDefault().Institution,
                    Department = employee.Employees.FirstOrDefault().Department,
                    Position = employee.Employees.FirstOrDefault().Position,
                    ResearchField = employee.Employees.FirstOrDefault().ResearchField,
                };
            }
            return null;
        }

        public bool Update(int id, EmployeeModel inputEmployee)
        {
            var employee = dbContext.AccountForEmployees.Include(e => e.Employees).FirstOrDefault(e => e.IdAccount == id);
            if (employee != null)
            {
                employee.Username = inputEmployee.Username;
                employee.Password = inputEmployee.Password;
                employee.Status = inputEmployee.Status;

                employee.Employees.First().IdRole = inputEmployee.IdRole;
                employee.Employees.First().FirstName = inputEmployee.FirstName;
                employee.Employees.First().LastName = inputEmployee.LastName;
                employee.Employees.First().FullName = inputEmployee.FirstName;
                employee.Employees.First().IdCard = inputEmployee.IdCard;
                employee.Employees.First().DateOfBirth = inputEmployee.DateOfBirth;
                employee.Employees.First().Gender = inputEmployee.Gender;
                employee.Employees.First().Email = inputEmployee.Email;
                employee.Employees.First().PhoneNumber = inputEmployee.PhoneNumber;
                employee.Employees.First().Degree = inputEmployee.Degree;
                employee.Employees.First().Addresss = inputEmployee.Addresss;
                employee.Employees.First().JoinDate = inputEmployee.JoinDate;
                employee.Employees.First().Institution = inputEmployee.Institution;
                employee.Employees.First().Department = inputEmployee.Department;
                employee.Employees.First().Position = inputEmployee.Position;
                employee.Employees.First().ResearchField = inputEmployee.ResearchField;

                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
