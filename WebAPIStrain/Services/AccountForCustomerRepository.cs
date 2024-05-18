using Microsoft.EntityFrameworkCore;
using WebAPIStrain.Entities;
using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace WebAPIStrain.Services
{
    public class AccountForCustomerRepository : IAccountForCustomerRepository
    {
        private readonly IrtContext dbContext;

        public AccountForCustomerRepository(IrtContext context)
        {
            dbContext = context;
        }

        public AccountForCustomerVM Create(AccountForCustomerModel account)
        {
            var newAccount = new AccountForCustomer
            {
                IdCustomer = account.IdCustomer,
                Username = account.Username,
                Password = account.Password,
                Status = account.Status
            };
            dbContext.Add(newAccount);
            dbContext.SaveChanges();
            return new AccountForCustomerVM
            {
                IdCustomer = newAccount.IdCustomer,
                Username = newAccount.Username,
                Password = newAccount.Password,
                Status = newAccount.Status
            };
        }

        public bool Delete(string id)
        {
            var account = dbContext.AccountForCustomers.FirstOrDefault(p => p.IdCustomer == id);
            if (account != null)
            {
                dbContext.Remove(account);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<AccountForCustomerVM> GetAll()
        {
            var accounts = dbContext.AccountForCustomers.Select(p => new AccountForCustomerVM
            {
                IdCustomer = p.IdCustomer,
                Username = p.Username,
                Password = p.Password,
                Status = p.Status
            }).ToList();
            return accounts;
        }

        public AccountForCustomerVM GetById(string id)
        {
            var account = dbContext.AccountForCustomers.FirstOrDefault(p => p.IdCustomer == id);
            if (account != null)
            {
                return new AccountForCustomerVM
                {
                    IdCustomer = account.IdCustomer,
                    Username = account.Username,
                    Password = account.Password,
                    Status = account.Status
                };
            }
            return null;
        }

        public bool Update(string id, AccountForCustomerModel account)
        {
            var _account = dbContext.AccountForCustomers.FirstOrDefault(p => p.IdCustomer == id);
            if (_account != null)
            {
                _account.Username = account.Username;
                _account.Password = account.Password;
                _account.Status = account.Status;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}