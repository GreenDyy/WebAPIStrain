using Microsoft.EntityFrameworkCore;
using WebAPIStrain.Entities;
using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace WebAPIStrain.Services
{
    public class PartnerRepository : IPartnerRepository
    {
        private readonly IrtContext dbContext;

        public PartnerRepository(IrtContext context)
        {
            dbContext = context;
        }

        public PartnerVM Create(PartnerModel partner)
        {
            var newPartner = new Partner
            {
                NameCompany = partner.NameCompany,
                AddressCompany = partner.AddressCompany,
                NamePartner = partner.NamePartner,
                Position = partner.Position,
                PhoneNumber = partner.PhoneNumber,
                BankNumber = partner.BankNumber,
                BankName = partner.BankName,
                QhnsNumber = partner.QhnsNumber,
                NameWard = partner.NameWard,
                NameDistrict = partner.NameDistrict,
                NameProvince = partner.NameProvince,
            };
            dbContext.Add(newPartner);
            dbContext.SaveChanges();
            return new PartnerVM
            {
                IdPartner = newPartner.IdPartner,
                NameCompany = newPartner.NameCompany,
                AddressCompany = newPartner.AddressCompany,
                NamePartner = newPartner.NamePartner,
                Position = newPartner.Position,
                PhoneNumber = newPartner.PhoneNumber,
                BankNumber = newPartner.BankNumber,
                BankName = newPartner.BankName,
                QhnsNumber = newPartner.QhnsNumber,
                NameWard = newPartner.NameWard,
                NameDistrict = newPartner.NameDistrict,
                NameProvince = newPartner.NameProvince,
            };
        }

        public bool Delete(int id)
        {
            var partner = dbContext.Partners.FirstOrDefault(p => p.IdPartner == id);
            if (partner != null)
            {
                dbContext.Remove(partner);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<PartnerVM> GetAll()
        {
            var partners = dbContext.Partners.Select(p => new PartnerVM
            {
                IdPartner = p.IdPartner,
                NameCompany = p.NameCompany,
                AddressCompany = p.AddressCompany,
                NamePartner = p.NamePartner,
                Position = p.Position,
                PhoneNumber = p.PhoneNumber,
                BankNumber = p.BankNumber,
                BankName = p.BankName,
                QhnsNumber = p.QhnsNumber,
                NameWard = p.NameWard,
                NameDistrict = p.NameDistrict,
                NameProvince = p.NameProvince,
            }).ToList();
            return partners;
        }

        public PartnerVM GetById(int id)
        {
            var partner = dbContext.Partners.FirstOrDefault(p => p.IdPartner == id);
            if (partner != null)
            {
                return new PartnerVM
                {
                    IdPartner = partner.IdPartner,
                    NameCompany = partner.NameCompany,
                    AddressCompany = partner.AddressCompany,
                    NamePartner = partner.NamePartner,
                    Position = partner.Position,
                    PhoneNumber = partner.PhoneNumber,
                    BankNumber = partner.BankNumber,
                    BankName = partner.BankName,
                    QhnsNumber = partner.QhnsNumber,
                    NameWard= partner.NameWard,
                    NameDistrict= partner.NameDistrict,
                    NameProvince = partner.NameProvince,
                };
            }
            return null;
        }

        public bool Update(int id, PartnerModel partner)
        {
            var _partner = dbContext.Partners.FirstOrDefault(p => p.IdPartner == id);
            if (_partner != null)
            {
                _partner.NameCompany = partner.NameCompany;
                _partner.AddressCompany = partner.AddressCompany;
                _partner.NamePartner = partner.NamePartner;
                _partner.Position = partner.Position;
                _partner.PhoneNumber = partner.PhoneNumber;
                _partner.BankNumber = partner.BankNumber;
                _partner.BankName = partner.BankName;
                _partner.QhnsNumber = partner.QhnsNumber;
                _partner.NameWard = partner.NameWard;
                _partner.NameDistrict = partner.NameDistrict;
                _partner.NameProvince = partner.NameProvince;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}