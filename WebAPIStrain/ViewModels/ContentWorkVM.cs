namespace WebAPIStrain.ViewModels
{
    public class ContentWorkVM
    {
        public int IdContentWork { get; set; }
        public int? IdProjectContent { get; set; }
        public string? IdEmployee { get; set; }
        public string? NameContent { get; set; }
        public string? Results { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? ContractNo { get; set; }
        public string? Status { get; set; }
        public string? Priority { get; set; }
        public DateOnly? EndDateActual { get; set; }
        public string? Notificattion { get; set; }
        public int? Title { get; set; }
        public int? SubTitle { get; set; }
        public byte[]? FileSaved { get; set; }
        public string? FileName { get; set; }
        public string? Histories { get; set; }
    }
}