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

        public bool Update(string id, EmployeeModel inputEmployee)
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
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(inputEmployee.Password.Trim());
                employee.AccountForEmployee.Password = hashedPassword;

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
                bool isPasswordMatch = BCrypt.Net.BCrypt.Verify(login.Password.Trim(), account.Password);
                if (isPasswordMatch)
                {
                    var employeeInfo = dbContext.Employees
                        .Join(dbContext.RoleForEmployees,
                            e => e.IdRole,
                            r => r.IdRole,
                            (e, r) => new { Employee = e, Role = r })
                        .FirstOrDefault(joined => joined.Employee.IdEmployee == account.IdEmployee);

                    if (employeeInfo != null)
                    {
                        return new EmployeeVM
                        {
                            IdEmployee = employeeInfo.Employee.IdEmployee,
                            IdRole = employeeInfo.Employee.IdRole,
                            FirstName = employeeInfo.Employee.FirstName,
                            LastName = employeeInfo.Employee.LastName,
                            FullName = employeeInfo.Employee.FullName,
                            IdCard = employeeInfo.Employee.IdCard,
                            DateOfBirth = employeeInfo.Employee.DateOfBirth,
                            Gender = employeeInfo.Employee.Gender,
                            Email = employeeInfo.Employee.Email,
                            PhoneNumber = employeeInfo.Employee.PhoneNumber,
                            Degree = employeeInfo.Employee.Degree,
                            Address = employeeInfo.Employee.Address,
                            JoinDate = employeeInfo.Employee.JoinDate,
                            ImageEmployee = employeeInfo.Employee.ImageEmployee,
                            NameWard = employeeInfo.Employee.NameWard,
                            NameDistrict = employeeInfo.Employee.NameDistrict,
                            NameProvince = employeeInfo.Employee.NameProvince,

                            Username = account.Username,
                            Password = account.Password,
                            Status = account.Status,

                            RoleName = employeeInfo.Role.RoleName
                        };
                    }
                }
            }
            return null;
        }

        public bool UpdateDataNoPass(string id, EmployeeModel inputEmployee)
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
        public bool UpdatePass(string id, EmployeeModel inputEmployee)
        {
            var employee = dbContext.Employees.Include(e => e.AccountForEmployee).FirstOrDefault(e => e.IdEmployee == id);
            if (employee != null)
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(inputEmployee.Password);
                employee.AccountForEmployee.Password = hashedPassword;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public bool PatchPasswordEmployee(string id, string password)
        {
            var employee = dbContext.Employees.Include(e => e.AccountForEmployee).FirstOrDefault(e => e.IdEmployee == id);
            if (employee != null)
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
                employee.AccountForEmployee.Password = hashedPassword;
                dbContext.SaveChanges();
                return true;
            }
            else
                return false;
        }
        public List<EmployeeVM> GetAllEmployeeByIdProject(string idProject)
        {
            var employees = (from cw in dbContext.ContentWorks
                             join pc in dbContext.ProjectContents on cw.IdProjectContent equals pc.IdProjectContent
                             join p in dbContext.Projects on pc.IdProject equals p.IdProject
                             join e in dbContext.Employees on cw.IdEmployee equals e.IdEmployee
                             join r in dbContext.RoleForEmployees on e.IdRole equals r.IdRole
                             where p.IdProject == idProject
                             select new EmployeeVM
                             {
                                 IdEmployee = e.IdEmployee,
                                 Address = e.Address,
                                 DateOfBirth = e.DateOfBirth,
                                 Degree = e.Degree,
                                 FirstName = e.FirstName,
                                 LastName = e.LastName,
                                 Email = e.Email,
                                 FullName = e.FullName,
                                 Gender = e.Gender,
                                 IdCard = e.IdCard,
                                 IdRole = e.IdRole,
                                 ImageEmployee = e.ImageEmployee,
                                 JoinDate = e.JoinDate,
                                 PhoneNumber = e.PhoneNumber,
                                 RoleName = r.RoleName,
                             })
                             .Distinct()
                             .ToList();

            return employees;
        }
    }
}
