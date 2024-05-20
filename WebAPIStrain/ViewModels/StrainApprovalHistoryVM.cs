using WebAPIStrain.Entities;

namespace WebAPIStrain.ViewModels
{
    public class StrainApprovalHistoryVM
    {
        public int Id { get; set; }

        public int? IdStrain { get; set; }

        public string? Status { get; set; }

        public DateOnly? DateApproval { get; set; }

        public string? Reason { get; set; }

    }
}
