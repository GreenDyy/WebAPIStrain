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
            string newIdBill;
            var lastBill = dbContext.Bills.OrderByDescending(b => b.IdBill).FirstOrDefault();
            if (lastBill != null)
            {
                string lastIdBill = lastBill.IdBill;
                string partNumberId = lastIdBill.Substring(2);
                int number = int.Parse(partNumberId);
                number++;
                newIdBill = "HD" + number.ToString("D7");
            }
            else
            {
                newIdBill = "HD0000001";
            }

            var newBill = new Bill
            {
                IdBill = newIdBill,
                IdOrder = bill.IdOrder,
                IdCustomer = bill.IdCustomer,
                IdEmployee = bill.IdEmployee,
                BillDate = bill.BillDate,
                StatusOfBill = bill.StatusOfBill,
                TypeOfBill = bill.TypeOfBill,
                TotalPrice = bill.Total
            };

            dbContext.Add(newBill);
            dbContext.SaveChanges();

            return new BillVM
            {
                IdBill = newBill.IdBill,
                IdOrder= newBill.IdOrder,
                IdCustomer = newBill.IdCustomer,
                IdEmployee = newBill.IdEmployee,
                BillDate = newBill.BillDate,
                StatusOfBill = newBill.StatusOfBill,
                TypeOfBill = newBill.TypeOfBill,
                TotalPrice = newBill.TotalPrice
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
                IdOrder = p.IdOrder,
                IdCustomer = p.IdCustomer,
                IdEmployee = p.IdEmployee,
                BillDate = p.BillDate,
                StatusOfBill = p.StatusOfBill,
                TypeOfBill = p.TypeOfBill,
                TotalPrice = p.TotalPrice
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
                    IdOrder = bill.IdOrder,
                    IdCustomer = bill.IdCustomer,
                    IdEmployee = bill.IdEmployee,
                    BillDate = bill.BillDate,
                    StatusOfBill = bill.StatusOfBill,
                    TypeOfBill = bill.TypeOfBill,
                    TotalPrice = bill.TotalPrice
                };
            }
            return null;
        }

        public bool Update(string id, BillModel bill)
        {
            var _bill = dbContext.Bills.FirstOrDefault(p => p.IdBill == id);
            if (_bill != null)
            {
                _bill.IdOrder = bill.IdOrder;
                _bill.IdCustomer = bill.IdCustomer;
                _bill.IdEmployee = bill.IdEmployee;
                _bill.BillDate = bill.BillDate;
                _bill.StatusOfBill = bill.StatusOfBill;
                _bill.TypeOfBill = bill.TypeOfBill;
                _bill.TotalPrice = bill.Total;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateStatusPayBill(string idBill, string status)
        {
            var _strain = dbContext.Bills.FirstOrDefault(s => s.IdBill == idBill);
            if (_strain != null)
            {
                _strain.StatusOfBill = status;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}