using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMC6.Shared
{
    public class LoginRespone
    {
        public bool SuccsessFull {  get; set; }
        public string Error { get; set; }
        public string Token {  get; set; }
    }
}
