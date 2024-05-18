namespace WebAPIStrain.ViewModels
{
    public class ProjectVM
    {
        public string IdProject { get; set; }
        public string? IdEmployee { get; set; }
        public int? IdPartner { get; set; }
        public string? ProjectName { get; set; }
        public string? Results { get; set; }
        public DateOnly? StartDateProject { get; set; }
        public string? ContractNo { get; set; }
        public string? Description { get; set; }
        public byte[]? FileProject { get; set; }
        public string? Status { get; set; }
    }
}
