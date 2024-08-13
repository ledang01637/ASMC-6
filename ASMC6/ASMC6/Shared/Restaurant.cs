using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMC6.Shared
{
    public class Restaurant
    {
        [Key]
        public int RestaurantId { get; set; }
        public int UserId {  get; set; }
        [Required(ErrorMessage = "Không được bỏ trống thông tin")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống thông tin")]
        public string Image { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống thông tin")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống thông tin")]
        public DateTime OpeningHours { get; set; } = DateTime.Now;
        public bool IsDelete { get; set; }

        public ICollection<Menu> Menus { get; set; }
    }
}
