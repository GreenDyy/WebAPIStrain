namespace WebAPIStrain.Models
{
    public class ProjectModel
    {
        public string? IdEmployee { get; set; }
        public int? IdPartner { get; set; }
        public string? ProjectName { get; set; }
        public string? Results { get; set; }
        public DateOnly? StartDateProject { get; set; }
        public DateOnly? EndDateProject { get; set; }
        public string? ContractNo { get; set; }
        public string? Description { get; set; }
        public byte[]? FileProject { get; set; }
        public string? FileName { get; set; }
        public string? Status { get; set; }
    }
}