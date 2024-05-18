using Microsoft.EntityFrameworkCore;
using WebAPIStrain.Entities;
using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace WebAPIStrain.Services
{
    public class BillDetailRepository : IBillDetailRepository
    {
        private readonly IrtContext dbContext;

        public BillDetailRepository(IrtContext context)
        {
            dbContext = context;
        }

        public BillDetailVM Create(BillDetailModel billDetail)
        {
            var newBillDetail = new BillDetail
            {
                IdBill = billDetail.IdBill,
                IdStrain = billDetail.IdStrain,
                Quantity = billDetail.Quantity
            };
            dbContext.Add(newBillDetail);
            dbContext.SaveChanges();
            return new BillDetailVM
            {
                IdBillDetail = newBillDetail.IdBillDetail,
                IdBill = newBillDetail.IdBill,
                IdStrain = newBillDetail.IdStrain,
                Quantity = newBillDetail.Quantity
            };
        }

        public bool Delete(int id)
        {
            var billDetail = dbContext.BillDetails.FirstOrDefault(p => p.IdBillDetail == id);
            if (billDetail != null)
            {
                dbContext.Remove(billDetail);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<BillDetailVM> GetAll()
        {
            var billDetails = dbContext.BillDetails.Select(p => new BillDetailVM
            {
                IdBillDetail = p.IdBillDetail,
                IdBill = p.IdBill,
                IdStrain = p.IdStrain,
                Quantity = p.Quantity
            }).ToList();
            return billDetails;
        }

        public BillDetailVM GetById(int id)
        {
            var billDetail = dbContext.BillDetails.FirstOrDefault(p => p.IdBillDetail == id);
            if (billDetail != null)
            {
                return new BillDetailVM
                {
                    IdBillDetail = billDetail.IdBillDetail,
                    IdBill = billDetail.IdBill,
                    IdStrain = billDetail.IdStrain,
                    Quantity = billDetail.Quantity
                };
            }
            return null;
        }

        public bool Update(int id, BillDetailModel billDetail)
        {
            var _billDetail = dbContext.BillDetails.FirstOrDefault(p => p.IdBillDetail == id);
            if (_billDetail != null)
            {
                _billDetail.IdBill = billDetail.IdBill;
                _billDetail.IdStrain = billDetail.IdStrain;
                _billDetail.Quantity = billDetail.Quantity;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}