using System.ComponentModel.DataAnnotations;

namespace Ecommerce1.DTO
{
    public class RegUserDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string Cpassword { get; set; }

        [Required]
        public string Email { get; set; }

        public int Age { get; set; }
        public string Phone { get; set; }





    }
}
