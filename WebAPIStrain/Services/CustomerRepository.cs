﻿using Microsoft.EntityFrameworkCore;
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
            // Kiểm tra tài khoản tồn tại
            var ac = dbContext.AccountForCustomers.FirstOrDefault(a => a.Username == inputCustomer.Username);
            if (ac != null)
            {
                return null;
            }

            // Xử lý IdCustomer mới
            string newIdCustomer;
            var lastCustomer = dbContext.Customers.OrderByDescending(c => c.IdCustomer).FirstOrDefault();
            if (lastCustomer != null)
            {
                string lastIdCustomer = lastCustomer.IdCustomer;
                string partNumberId = lastIdCustomer.Substring(2); // Bỏ "KH"
                int number = int.Parse(partNumberId);
                number++;
                newIdCustomer = "KH" + number.ToString("D5");
            }
            else
            {
                newIdCustomer = "KH00001";
            }

            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    // Tạo Customer mới
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
                        NameWard = inputCustomer.NameWard,
                        NameDistrict = inputCustomer.NameDistrict,
                        NameProvince = inputCustomer.NameProvince,
                    };
                    dbContext.Add(newCustomer);
                    dbContext.SaveChanges();

                    // Tạo Account mới
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(inputCustomer.Password);
                    var newAccount = new AccountForCustomer
                    {
                        IdCustomer = newCustomer.IdCustomer,
                        Username = inputCustomer.Username,
                        Password = hashedPassword,
                        Status = inputCustomer.Status
                    };
                    dbContext.Add(newAccount);
                    dbContext.SaveChanges();

                    // Tạo Cart mới
                    var newCart = new Cart
                    {
                        IdCustomer = newCustomer.IdCustomer,
                        TotalProduct = 0,
                    };
                    dbContext.Carts.Add(newCart);
                    dbContext.SaveChanges();

                    // Commit transaction
                    transaction.Commit();

                    // Trả về CustomerVM
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
                        NameWard = newCustomer.NameWard,
                        NameDistrict = newCustomer.NameDistrict,
                        NameProvince = newCustomer.NameProvince,

                        Username = newAccount.Username,
                        Password = newAccount.Password,
                        Status = newAccount.Status,
                    };
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public bool Delete(string id)
        {
            var customer = dbContext.Customers
                .Include(c => c.AccountForCustomer)
                .Include(c => c.Carts)
                .FirstOrDefault(s => s.IdCustomer == id);

            if (customer != null)
            {
                if (customer.AccountForCustomer != null)
                {
                    dbContext.AccountForCustomers.Remove(customer.AccountForCustomer);
                }
                if (customer.Carts != null)
                {
                    foreach (var cart in customer.Carts)
                    {
                        dbContext.Carts.Remove(cart);
                    }
                }
                dbContext.Customers.Remove(customer);
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
                NameWard = c.NameWard,
                NameDistrict = c.NameDistrict,
                NameProvince = c.NameProvince,
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
                    NameWard = customer.NameWard,
                    NameDistrict = customer.NameDistrict,
                    NameProvince = customer.NameProvince,
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
                _customer.NameWard = customer.NameWard;
                _customer.NameDistrict = customer.NameDistrict;
                _customer.NameProvince = customer.NameProvince;
                _customer.AccountForCustomer.Username = customer.Username;
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(customer.Password);
                _customer.AccountForCustomer.Password = hashedPassword;
                _customer.AccountForCustomer.Status = customer.Status;

                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool ChangePass(string id, CustomerModel customer)
        {
            var _customer = dbContext.Customers.Include(c => c.AccountForCustomer).FirstOrDefault(c => c.IdCustomer == id);
            if (_customer != null)
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(customer.Password);
                _customer.AccountForCustomer.Password = hashedPassword;

                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateDataNoPass(string id, CustomerModel customer)
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
                _customer.NameWard = customer.NameWard;
                _customer.NameDistrict = customer.NameDistrict;
                _customer.NameProvince = customer.NameProvince;
                _customer.AccountForCustomer.Username = customer.Username;
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
                bool isPasswordMatch = BCrypt.Net.BCrypt.Verify(login.Password, account.Password);
                if (isPasswordMatch)
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
                        NameWard = profile.NameWard,
                        NameDistrict = profile.NameDistrict,
                        NameProvince = profile.NameProvince,

                        Username = account.Username,
                        Password = account.Password,
                        Status = account.Status,
                    };
                }
                return null;
            }
            return null;
        }
        public bool ResetPassword(string email, string newPass)
        {
            var customer = dbContext.Customers.FirstOrDefault(c => c.Email == email);
            if (customer != null)
            {
                var account = dbContext.AccountForCustomers.FirstOrDefault(a => a.IdCustomer == customer.IdCustomer);
                if (account != null)
                {
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPass);
                    account.Password = hashedPassword;
                    dbContext.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        public bool CheckExistEmail(string email)
        {
            var customer = dbContext.Customers.FirstOrDefault(c => c.Email == email);
            if (customer != null)
            {
                return true;
            }
            return false;
        }

        public bool CheckExistUserName(string userName)
        {
            var acc = dbContext.AccountForCustomers.FirstOrDefault(a => a.Username == userName);
            if (acc != null)
            {
                return true;
            }
            return false;
        }

        public bool CheckExistEmailWithoutSelf(string email, string idCustomer)
        {
            var emailExists = dbContext.Customers
           .Any(c => c.Email == email && c.IdCustomer != idCustomer);

            return emailExists;
        }

        public CustomerVM LoginWithGoogle(string email)
        {
            var profile = dbContext.Customers.Include(c => c.AccountForCustomer).FirstOrDefault(c => c.Email == email);
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
                NameWard = profile.NameWard,
                NameDistrict = profile.NameDistrict,
                NameProvince = profile.NameProvince,

                Username = profile.AccountForCustomer.Username,
                Password = profile.AccountForCustomer.Password,
                Status = profile.AccountForCustomer.Status,

            };
        }

        public bool LockAccount(string idCustomer)
        {
            var profile = dbContext.Customers.Include(c => c.AccountForCustomer).FirstOrDefault(c => c.IdCustomer == idCustomer);
            if (profile != null)
            {
                profile.AccountForCustomer.Status = "Tài khoản bị khóa";
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool OpenAccount(string idCustomer)
        {
            var profile = dbContext.Customers.Include(c => c.AccountForCustomer).FirstOrDefault(c => c.IdCustomer == idCustomer);
            if (profile != null)
            {
                profile.AccountForCustomer.Status = "Đang hoạt động";
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
