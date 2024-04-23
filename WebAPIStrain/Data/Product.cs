using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIStrain.Data
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int MaSanPham { get; set; }
        [Required]
        [MaxLength(100)]
        public string TenSanPham { get; set; }
        public string GioiTinh { get; set; }
        public string XuatXu { get; set; }
        public string MoTa { get; set; }
        public float GiaGoc { get; set; }
        public float GiaGiam { get; set; }
        public int MaDanhMuc { get; set; }
        public int MaThuongHieu { get; set; }
    }
}
