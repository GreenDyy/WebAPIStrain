using WebAPIStrain.Entities;
using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;

namespace WebAPIStrain.Services
{
    public class StrainApprovalHistoryRepository : IStrainApprovalHistoryRepository
    {
        private readonly IrtContext dbContext;

        public StrainApprovalHistoryRepository(IrtContext context)
        {
            dbContext = context;
        }

        public StrainApprovalHistoryVM Create(StrainApprovalHistoryModel strainApprovalHistory)
        {
            var newStrainApprovalHistory = new StrainApprovalHistory
            {
                IdStrain = strainApprovalHistory.IdStrain,
                Status = strainApprovalHistory.Status,
                DateApproval = strainApprovalHistory.DateApproval,
                Reason = strainApprovalHistory.Reason,
            };
            dbContext.Add(newStrainApprovalHistory);
            dbContext.SaveChanges();
            return new StrainApprovalHistoryVM
            {
                Id = newStrainApprovalHistory.Id,
                IdStrain = newStrainApprovalHistory.IdStrain,
                Status = newStrainApprovalHistory.Status,
                DateApproval = newStrainApprovalHistory.DateApproval,
                Reason = newStrainApprovalHistory.Reason,
            };
        }

        public bool Delete(int idStrain)
        {
            var strainApprovalHistory = dbContext.StrainApprovalHistories.FirstOrDefault(p => p.IdStrain == idStrain);
            if (strainApprovalHistory != null)
            {
                dbContext.Remove(strainApprovalHistory);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<StrainApprovalHistoryVM> GetAll()
        {
            var StrainApprovalHistorys = dbContext.StrainApprovalHistories.Select(p => new StrainApprovalHistoryVM
            {
                Id = p.Id,
                IdStrain = p.IdStrain,
                Status = p.Status,
                DateApproval = p.DateApproval,
                Reason = p.Reason
            }).ToList();
            return StrainApprovalHistorys;
        }

        public StrainApprovalHistoryVM GetById(int idStrain)
        {
            var strainApprovalHistory = dbContext.StrainApprovalHistories.FirstOrDefault(p => p.IdStrain == idStrain);
            if (strainApprovalHistory != null)
            {
                return new StrainApprovalHistoryVM
                {
                    Id = strainApprovalHistory.Id,
                    IdStrain = strainApprovalHistory.IdStrain,
                    Status = strainApprovalHistory.Status,
                    DateApproval = strainApprovalHistory.DateApproval,
                    Reason = strainApprovalHistory.Reason
                };
            }
            return null;
        }

        public bool Update(int idStrain, StrainApprovalHistoryModel strainApprovalHistory)
        {
            var _strainApprovalHistory = dbContext.StrainApprovalHistories.FirstOrDefault(p => p.IdStrain == idStrain);
            if (_strainApprovalHistory != null)
            {
                _strainApprovalHistory.Status = strainApprovalHistory.Status;
                _strainApprovalHistory.DateApproval = strainApprovalHistory.DateApproval;
                _strainApprovalHistory.Reason = strainApprovalHistory.Reason;

                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
