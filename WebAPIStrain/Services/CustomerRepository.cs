using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Security.Principal;
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
            var lastCustomer = dbContext.Customers.OrderBy(c => c.IdCustomer).LastOrDefault();
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
                PhoneNumber = inputCustomer.PhoneNumber,
                Address = inputCustomer.Address,
                Image = inputCustomer.Image,
            };
            dbContext.Add(newCustomer);
            dbContext.SaveChanges();

            //account
            //string hashedPassword = BCrypt.Net.BCrypt.HashPassword(inputCustomer.Password);

            var newAccount = new AccountForCustomer
            {
                IdCustomer = newCustomer.IdCustomer, //lấy id dc generate từ customer vừa add ở trên
                Username = inputCustomer.Username,
                //Password = hashedPassword,
                Password = inputCustomer.Password,
                Status = inputCustomer.Status
            };
            dbContext.Add(newAccount);
            dbContext.SaveChanges();

            //tạo cart
            var newCart = new Cart
            {
                IdCustomer = newCustomer.IdCustomer,
                TotalProduct = 0,
            };
            dbContext.Carts.Add(newCart);
            dbContext.SaveChanges();

            //truy vào csdl lấy data mới thêm, dùng VM
            return new CustomerVM
            {
                IdCustomer = newCustomer.IdCustomer,
                FirstName = newCustomer.FirstName,
                LastName = newCustomer.LastName,
                FullName = newCustomer.FullName,
                DateOfBirth = newCustomer.DateOfBirth,
                Gender = newCustomer.Gender,
                Email = newCustomer.Email,
                PhoneNumber = newCustomer.PhoneNumber,
                Address = newCustomer.Address,
                Image = newCustomer.Image,

                Username = newAccount.Username,
                Password = newAccount.Password,
                Status = newAccount.Status,
            };
        }

        public bool Delete(string id)
        {
            var customer = dbContext.Customers.Include(c => c.AccountForCustomer).FirstOrDefault(s => s.IdCustomer == id);
            if (customer != null)
            {
                dbContext.Remove(customer.AccountForCustomer);
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
                IdCustomer = c.IdCustomer,
                FirstName = c.FirstName,
                LastName = c.LastName,
                FullName = c.FullName,
                DateOfBirth = c.DateOfBirth,
                Gender = c.Gender,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                Address = c.Address,
                Image = c.Image,
                Username = c.AccountForCustomer.Username,
                Password = c.AccountForCustomer.Password,
                Status = c.AccountForCustomer.Status
                
            }).ToList();
            return customers;
        }

        public CustomerVM GetById(string id)
        {
            var customer = dbContext.Customers.Include(c => c.AccountForCustomer).FirstOrDefault(c => c.IdCustomer == id);
            if (customer != null)
            {
                return new CustomerVM
                {
                    IdCustomer = customer.IdCustomer,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    FullName = customer.FullName,
                    DateOfBirth = customer.DateOfBirth,
                    Gender = customer.Gender,
                    Email = customer.Email,
                    PhoneNumber = customer.PhoneNumber,
                    Address = customer.Address,
                    Image = customer.Image,
                    Username = customer.AccountForCustomer.Username,
                    Password = customer.AccountForCustomer.Password,
                    Status = customer.AccountForCustomer.Status
                };
            }
            return null;
        }

        public bool Update(string id, CustomerModel customer)
        {
            var _customer = dbContext.Customers.Include(c => c.AccountForCustomer).FirstOrDefault(c => c.IdCustomer == id);
            if (_customer != null)
            {
                _customer.FirstName = customer.FirstName;
                _customer.LastName = customer.LastName;
                _customer.FullName = customer.LastName + " " + customer.FirstName;
                _customer.DateOfBirth = customer.DateOfBirth;
                _customer.Gender = customer.Gender;
                _customer.Email = customer.Email;
                _customer.PhoneNumber = customer.PhoneNumber;
                _customer.Address = customer.Address;
                _customer.Image = customer.Image;
                _customer.AccountForCustomer.Username = customer.Username;
                _customer.AccountForCustomer.Password = customer.Password;
                _customer.AccountForCustomer.Status = customer.Status;

                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public CustomerVM Login(Login login)
        {
            var account = dbContext.AccountForCustomers.FirstOrDefault(ac => ac.Username == login.Username);
            if (account != null)
            {
                //bool isPasswordMatch = BCrypt.Net.BCrypt.Verify(password, account.Password);
                //if (isPasswordMatch)
                //{
                //    return true;
                //} đợi sửa data pass custom thành mã hoá rồi dùng
                if (account.Password == login.Password)
                {
                    var profile = dbContext.Customers.FirstOrDefault(c => c.IdCustomer == account.IdCustomer);
                    return new CustomerVM
                    {
                        IdCustomer = profile.IdCustomer,
                        FirstName = profile.FirstName,
                        LastName = profile.LastName,
                        FullName = profile.FullName,
                        DateOfBirth = profile.DateOfBirth,
                        Gender = profile.Gender,
                        Email = profile.Email,
                        PhoneNumber = profile.PhoneNumber,
                        Address = profile.Address,
                        Image = profile.Image,

                        Username = account.Username,
                        Password = account.Password,
                        Status = account.Status,
                    };
                }
                return null;
            }
            return null;
        }
    }
}
