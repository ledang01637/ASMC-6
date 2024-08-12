using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMC6.Shared
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Chọn menu.")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn một menu hợp lệ.")]
        public int MenuId { get; set; }
        [Required(ErrorMessage = "Chọn danh mục")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn một danh mục hợp lệ.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Nhập tên.")]
        [StringLength(100, ErrorMessage = "Tên sản phẩm không được vượt quá 100 ký tự.")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Chọn ảnh")]
        public string Image { get; set; }
        [Required(ErrorMessage = "Nhập giá")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Giá sản phẩm phải lớn hơn 0.")]
        public decimal Price { get; set; }
        public bool IsDelete { get; set; }
        public Menu Menu { get; set; }
        public Category Category { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }


    }
}
