using WebAPIStrain.Models;
using WebAPIStrain.ViewModels;

namespace WebAPIStrain.Services
{
    public interface IStrainApprovalHistoryRepository
    {
        public List<StrainApprovalHistoryVM> GetAll();
        public StrainApprovalHistoryVM GetById(int idStrain);
        public StrainApprovalHistoryVM Create(StrainApprovalHistoryModel inputStrainApprovalHistory);
        public bool Update(int idStrain, StrainApprovalHistoryModel inputStrainApprovalHistory);
        public bool Delete(int idStrain);
    }
}
