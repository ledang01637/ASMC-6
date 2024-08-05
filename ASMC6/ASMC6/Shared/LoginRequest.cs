using System.ComponentModel.DataAnnotations;

namespace ASMC6.Shared
{
    public class LoginRequest
    {
        [Required]
        public string Email {  get; set; }

        [Required]
        public string Password { get; set; }
    }
}
