using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;

namespace WebAPIStrain.Services
{
    public interface IStrainApprovalHistoryRepository
    {
        public List<StrainApprovalHistoryVM> GetAll();
        public StrainApprovalHistoryVM GetById(int id);
        public StrainApprovalHistoryVM Create(StrainApprovalHistoryModel inputStrainApprovalHistory);
        public bool Update(int id, StrainApprovalHistoryModel inputStrainApprovalHistory);
        public bool Delete(int id);
    }
}
