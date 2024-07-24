using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMC6.Shared
{
    public class Menu
    {
        [Key]
        public int MenuId { get; set; }
        public int RestaurantId { get; set; }
        public string Name { get; set; }
        public bool IsDelete { get; set; }

        public Restaurant Restaurant { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
