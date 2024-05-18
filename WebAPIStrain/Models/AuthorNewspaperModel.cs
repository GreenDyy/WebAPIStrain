namespace WebAPIStrain.Models
{
    public class AuthorNewspaperModel
    {
        public int? IdNewspaper { get; set; }
        public string? IdEmployee { get; set; }
        public DateOnly? PostDate { get; set; }
        public string? RoleOfAuthor { get; set; }
    }
}