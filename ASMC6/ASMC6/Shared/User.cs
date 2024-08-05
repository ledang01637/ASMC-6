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
        public string Name { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]{3,}\.[a-zA-Z0-9-.]+$", ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password là bắt buộc")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Phone là bắt buộc")]
        [RegularExpression(@"^(0[3|5|7|8|9])+([0-9]{8})$", ErrorMessage = "Không đúng định dạng số điện thoại")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address là bắt buộc")]
        public string Address { get; set; }
        public bool IsDelete { get; set; }

        public Role Role { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
