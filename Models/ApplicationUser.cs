using Microsoft.AspNetCore.Identity;

namespace Ecommerce1.Models
{
    public class ApplicationUser:IdentityUser
    {

        public int Age { set; get; }
    }
}
