using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMC6.Shared
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public int RoleId {  get; set; }

        [Required(ErrorMessage = "Tên là bắt buộc")]
        [DataType(DataType.Text, ErrorMessage = "Không đúng định dạng dữ liệu vui lòng nhập lại")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]{3,}\.[a-zA-Z0-9-.]+$", ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password là bắt buộc")]
        [DataType(DataType.Password, ErrorMessage = "Không đúng định dạng dữ liệu vui lòng nhập lại")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Phone là bắt buộc")]
        [MaxLength(10, ErrorMessage = "Số điện thoại sai định dạng vui lòng nhập lại")]
        [MinLength(10, ErrorMessage = "Số điện thoại sai định dạng vui lòng nhập lại")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address là bắt buộc")]
        [DataType(DataType.Text, ErrorMessage = "Không đúng định dạng dữ liệu vui lòng nhập lại")]
        public string Address { get; set; }
        public bool IsDelete { get; set; }

        public Role Role { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
