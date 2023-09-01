using Microsoft.AspNetCore.Identity;

namespace CapstoneProject.Domain.Entities
{
    public class User : IdentityUser
    {
        public Mentor Mentor { get; set; }
        public Mentee Mentee { get; set; }
    }
}
