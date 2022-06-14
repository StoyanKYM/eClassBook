using Microsoft.AspNetCore.Identity;

namespace eClassBook.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string RoleName { get; set; }
    }
}