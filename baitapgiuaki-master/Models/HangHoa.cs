using System.ComponentModel.DataAnnotations;

namespace GoodsAPI.Models
{
    public class HangHoa
    {
        [Key]
        [StringLength(9)]
        public string MaHangHoa { get; set; }

        [Required]
        public string TenHangHoa { get; set; }

        public int SoLuong { get; set; }

        public string GhiChu { get; set; } // Thêm cột ghi chú
    }
}
