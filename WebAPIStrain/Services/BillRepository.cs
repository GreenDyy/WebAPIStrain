using Microsoft.EntityFrameworkCore;
using WebAPIStrain.Entities;
using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace WebAPIStrain.Services
{
    public class BillRepository : IBillRepository
    {
        private readonly IrtContext dbContext;

        public BillRepository(IrtContext context)
        {
            dbContext = context;
        }

        public BillVM Create(BillModel bill)
        {
            var newBill = new Bill
            {
                IdBill = bill.IdBill,
                IdCustomer = bill.IdCustomer,
                IdEmployee = bill.IdEmployee,
                BillDate = bill.BillDate,
                StatusOfBill = bill.StatusOfBill,
                TypeOfBill = bill.TypeOfBill,
                Total = bill.Total
            };
            dbContext.Add(newBill);
            dbContext.SaveChanges();
            return new BillVM
            {
                IdBill = newBill.IdBill,
                IdCustomer = newBill.IdCustomer,
                IdEmployee = newBill.IdEmployee,
                BillDate = newBill.BillDate,
                StatusOfBill = newBill.StatusOfBill,
                TypeOfBill = newBill.TypeOfBill,
                Total = newBill.Total
            };
        }

        public bool Delete(string id)
        {
            var bill = dbContext.Bills.FirstOrDefault(p => p.IdBill == id);
            if (bill != null)
            {
                dbContext.Remove(bill);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<BillVM> GetAll()
        {
            var bills = dbContext.Bills.Select(p => new BillVM
            {
                IdBill = p.IdBill,
                IdCustomer = p.IdCustomer,
                IdEmployee = p.IdEmployee,
                BillDate = p.BillDate,
                StatusOfBill = p.StatusOfBill,
                TypeOfBill = p.TypeOfBill,
                Total = p.Total
            }).ToList();
            return bills;
        }

        public BillVM GetById(string id)
        {
            var bill = dbContext.Bills.FirstOrDefault(p => p.IdBill == id);
            if (bill != null)
            {
                return new BillVM
                {
                    IdBill = bill.IdBill,
                    IdCustomer = bill.IdCustomer,
                    IdEmployee = bill.IdEmployee,
                    BillDate = bill.BillDate,
                    StatusOfBill = bill.StatusOfBill,
                    TypeOfBill = bill.TypeOfBill,
                    Total = bill.Total
                };
            }
            return null;
        }

        public bool Update(string id, BillModel bill)
        {
            var _bill = dbContext.Bills.FirstOrDefault(p => p.IdBill == id);
            if (_bill != null)
            {
                _bill.IdCustomer = bill.IdCustomer;
                _bill.IdEmployee = bill.IdEmployee;
                _bill.BillDate = bill.BillDate;
                _bill.StatusOfBill = bill.StatusOfBill;
                _bill.TypeOfBill = bill.TypeOfBill;
                _bill.Total = bill.Total;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}