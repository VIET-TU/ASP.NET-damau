using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NguyenVietTu_211200970.Models
{
    public partial class HangHoa
    {
        [Key]
        public int MaHang { get; set; }


        public int MaLoai { get; set; }

        [Required(ErrorMessage = "Tên hàng không được để trống.")]
        public string TenHang { get; set; }

        [Required(ErrorMessage = "Gia không được để trống.")]
        [Range(100, 5000, ErrorMessage = "Giá phải nằm trong khoảng từ 100 đến 5000")]
        public int? Gia { get; set; }

        [Required(ErrorMessage = "Anh không được để trống.")]
        [RegularExpression(@"^.*\.(jpg|JPG|gif|GIF|doc|DOC|pdf|PDF)$", ErrorMessage = "File không khợp lệ")]
        public string Anh { get; set; }

        public virtual LoaiHang? MaLoaiNavigation { get; set; }
    }
}
