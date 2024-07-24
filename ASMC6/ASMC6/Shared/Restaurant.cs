using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMC6.Shared
{
    public class Restaurant
    {
        public int RestaurantId { get; set; }
        public int UserId {  get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public DateTime OpeningHours { get; set; }
        public bool IsDelete { get; set; }

        public ICollection<Menu> Menus { get; set; }
    }
}
