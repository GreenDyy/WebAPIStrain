//using WebAPIStrain.Entities;
//using WebAPIStrain.Models;
//using WebAPIStrain.ViewModels;

//namespace WebAPIStrain.Services
//{
//    public class StrainApprovalHistoryRepository
//    {
//        private readonly IrtContext dbContext;

//        public StrainApprovalHistoryRepository(IrtContext context) { 
//            dbContext = context;
//        }

//        public StrainApprovalHistoryVM Create(StrainApprovalHistoryModel strainApprovalHistory)
//        {
//            var newStrainApprovalHistory = new StrainApprovalHistory
//            {
//                IdStrain = strainApprovalHistory.IdStrain,
//                Status = strainApprovalHistory.Status,  
//                DateApproval = strainApprovalHistory.DateApproval,
//                Reason = strainApprovalHistory.Reason,
//            };
//            dbContext.Add(newStrainApprovalHistory);
//            dbContext.SaveChanges();
//            return new StrainApprovalHistoryVM
//            {
//                Id = newStrainApprovalHistory.Id,
//                IdStrain = newStrainApprovalHistory.IdStrain,
//                Status = newStrainApprovalHistory.Status,
//                DateApproval = newStrainApprovalHistory.DateApproval,
//                Reason = newStrainApprovalHistory.Reason,
//            };
//        }

//        public bool Delete(int id)
//        {
//            var StrainApprovalHistory = dbContext.StrainApprovalHistories.FirstOrDefault(p => p.Id == id);
//            if (StrainApprovalHistory != null)
//            {
//                dbContext.Remove(StrainApprovalHistory);
//                dbContext.SaveChanges();
//                return true;
//            }
//            return false;
//        }

//        public List<StrainApprovalHistoryVM> GetAll()
//        {
//            var StrainApprovalHistorys = dbContext.StrainApprovalHistorys.Select(p => new StrainApprovalHistoryVM
//            {
//                IdStrainApprovalHistory = p.IdStrainApprovalHistory,
//                NameStrainApprovalHistory = p.NameStrainApprovalHistory
//            }).ToList();
//            return StrainApprovalHistorys;
//        }

//        public StrainApprovalHistoryVM GetById(int id)
//        {
//            var StrainApprovalHistory = dbContext.StrainApprovalHistorys.FirstOrDefault(p => p.IdStrainApprovalHistory == id);
//            if (StrainApprovalHistory != null)
//            {
//                return new StrainApprovalHistoryVM
//                {
//                    IdStrainApprovalHistory = StrainApprovalHistory.IdStrainApprovalHistory,
//                    NameStrainApprovalHistory = StrainApprovalHistory.NameStrainApprovalHistory
//                };
//            }
//            return null;
//        }

//        public bool Update(int id, StrainApprovalHistoryModel StrainApprovalHistory)
//        {
//            var _StrainApprovalHistory = dbContext.StrainApprovalHistorys.FirstOrDefault(p => p.IdStrainApprovalHistory == id);
//            if (_StrainApprovalHistory != null)
//            {
//                _StrainApprovalHistory.NameStrainApprovalHistory = StrainApprovalHistory.NameStrainApprovalHistory;

//                dbContext.SaveChanges();
//                return true;
//            }
//            return false;
//        }
//    }
//}
