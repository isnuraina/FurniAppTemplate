using Microsoft.AspNetCore.Identity;

namespace FurniAppTemplate.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}
