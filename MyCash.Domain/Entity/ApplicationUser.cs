using Microsoft.AspNetCore.Identity;

namespace MyCash.Domain.Entity
{
    public class ApplicationUser :IdentityUser
    {
        public string Name { get; set; }
    }
}
