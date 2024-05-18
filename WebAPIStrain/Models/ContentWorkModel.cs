namespace WebAPIStrain.Models
{
    public class ContentWorkModel
    {
        public int? IdProjectContent { get; set; }
        public string? IdEmployee { get; set; }
        public string? NameContent { get; set; }
        public string? Results { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? ContractNo { get; set; }
        public string? Status { get; set; }
        public string? Priority { get; set; }
    }
}
