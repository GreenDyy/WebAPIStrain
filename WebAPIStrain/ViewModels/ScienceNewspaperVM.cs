namespace WebAPIStrain.ViewModels
{
    public class ScienceNewspaperVM
    {
        public int IdNewspaper { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateOnly? PostDate { get; set; }
        public byte[]? Image { get; set; }
        public string? IdEmployee { get; set; }
    }
}
