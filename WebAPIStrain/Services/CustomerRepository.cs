using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using WebAPIStrain.Entities;
using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;

namespace WebAPIStrain.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        public readonly IrtContext dbContext;
        public CustomerRepository(IrtContext context)
        {
            dbContext = context;
        }

        public CustomerVM Create(CustomerModel inputCustomer)
        {
            //"KH00001"
            //xử lý IdCustomer cuối trước khi thêm
            //các bước xử lý id: cắt chuỗi số -> convert sang int -> tăng 1 -> conver sang string dạng D5 -> ghép lại như cữ
            string newIdCustomer;
            string? lastIdCustomer;
            var lastCustomer = dbContext.Customers.OrderBy(c=>c.IdCustomer).LastOrDefault();
            if (lastCustomer != null)
            {
                lastIdCustomer = lastCustomer.IdCustomer;
                string partNumberId = lastIdCustomer.Substring(2); //00001
                int number = int.Parse(partNumberId);
                number++;
                partNumberId = number.ToString("D5");
                newIdCustomer = "KH" + partNumberId;
            }
            else
            {
                newIdCustomer = "KH00001";
            }

            //---
            var newCustomer = new Customer
            {
                IdCustomer = newIdCustomer,
                FirstName = inputCustomer.FirstName,
                LastName = inputCustomer.LastName,
                FullName = inputCustomer.LastName + " " + inputCustomer.FirstName,
                DateOfBirth = inputCustomer.DateOfBirth,
                Gender = inputCustomer.Gender,
                Email = inputCustomer.Email,
                PhoneNumber = inputCustomer.PhoneNumber
            };
            dbContext.Add(newCustomer);
            dbContext.SaveChanges();

            var newAccount = new AccountForCustomer
            {
                Id = newCustomer.Id, //lấy id dc generate từ customer vừa add ở trên
                Username = inputCustomer.Username,
                Password = inputCustomer.Password,
                Status = inputCustomer.Status
            };
            dbContext.Add(newAccount);
            dbContext.SaveChanges();
            //truy vào csdl lấy data mới thêm, dùng VM
            return new CustomerVM
            {
                Id = newCustomer.Id,
                IdCustomer = newCustomer.IdCustomer,
                FirstName = newCustomer.FirstName,
                LastName = newCustomer.LastName,
                FullName = newCustomer.FullName,
                DateOfBirth = newCustomer.DateOfBirth,
                Gender = newCustomer.Gender,
                Email = newCustomer.Email,
                PhoneNumber = newCustomer.PhoneNumber,

                IdAccountForCustomer = newAccount.IdAccountForCustomer,
                Username = newAccount.Username,
                Password = newAccount.Password,
                Status = newAccount.Status,
            };
        }

        public bool Delete(int id)
        {
            var customer = dbContext.Customers.Include(c => c.AccountForCustomers).FirstOrDefault(s => s.Id == id);
            if (customer != null)
            {
                dbContext.RemoveRange(customer.AccountForCustomers);
                dbContext.Remove(customer);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<CustomerVM> GetAll()
        {
            var customers = dbContext.Customers.Select(c => new CustomerVM
            {
                Id = c.Id,
                IdCustomer = c.IdCustomer,
                FirstName = c.FirstName,
                LastName = c.LastName,
                FullName = c.FullName,
                DateOfBirth = c.DateOfBirth,
                Gender = c.Gender,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                IdAccountForCustomer = c.AccountForCustomers.FirstOrDefault().IdAccountForCustomer,
                Username = c.AccountForCustomers.FirstOrDefault().Username,
                Password = c.AccountForCustomers.FirstOrDefault().Password,
                Status = c.AccountForCustomers.FirstOrDefault().Status
            }).ToList();
            return customers;
        }

        public CustomerVM GetById(int id)
        {
            var customer = dbContext.Customers.Include(c => c.AccountForCustomers).FirstOrDefault(c => c.Id == id);
            if (customer != null)
            {
                return new CustomerVM
                {
                    Id = customer.Id,
                    IdCustomer = customer.IdCustomer,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    FullName = customer.FullName,
                    DateOfBirth = customer.DateOfBirth,
                    Gender = customer.Gender,
                    Email = customer.Email,
                    PhoneNumber = customer.PhoneNumber,
                    IdAccountForCustomer = customer.AccountForCustomers.FirstOrDefault().IdAccountForCustomer,
                    Username = customer.AccountForCustomers.FirstOrDefault().Username,
                    Password = customer.AccountForCustomers.FirstOrDefault().Password,
                    Status = customer.AccountForCustomers.FirstOrDefault().Status
                };
            }
            return null;
        }

        public bool Update(int id, CustomerModel customer)
        {
            var _customer = dbContext.Customers.Include(c => c.AccountForCustomers).FirstOrDefault(c => c.Id == id);
            if (_customer != null)
            {
                _customer.FirstName = customer.FirstName;
                _customer.LastName = customer.LastName;
                _customer.FullName = customer.LastName + " " + customer.FirstName;
                _customer.DateOfBirth = customer.DateOfBirth;
                _customer.Gender = customer.Gender;
                _customer.Email = customer.Email;
                _customer.PhoneNumber = customer.PhoneNumber;
                _customer.AccountForCustomers.FirstOrDefault().Username = customer.Username;
                _customer.AccountForCustomers.FirstOrDefault().Password = customer.Password;
                _customer.AccountForCustomers.FirstOrDefault().Status = customer.Status;

                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
