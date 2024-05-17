using WebAPIStrain.Entities;

namespace WebAPIStrain.Models
{
    public class StrainApprovalHistoryModel
    {

        public int? IdStrain { get; set; }

        public string? Status { get; set; }

        public DateOnly? DateApproval { get; set; }

        public string? Reason { get; set; }

    }
}
