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
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool IsDelete { get; set; }

        public Role Role { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
