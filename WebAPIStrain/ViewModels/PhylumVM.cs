using WebAPIStrain.Entities;

namespace WebAPIStrain.ViewModels
{
    public class PhylumVM
    {
        public int IdPhylum { get; set; }

        public string? NamePhylum { get; set; }
        public virtual ICollection<ClassVM> Classes { get; set; } = new List<ClassVM>();
    }
}
