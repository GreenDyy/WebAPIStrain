namespace WebAPIStrain.ViewModels
{
    public class AuthorNewspaperVM
    {
        public int? IdNewspaper { get; set; }
        public string? IdEmployee { get; set; }
        public DateOnly? PostDate { get; set; }
        public string? RoleOfAuthor { get; set; }
    }
}