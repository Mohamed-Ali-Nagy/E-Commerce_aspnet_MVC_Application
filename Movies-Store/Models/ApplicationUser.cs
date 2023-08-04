using Microsoft.AspNetCore.Identity;

namespace Movies_Store.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}
