using CapstoneProject.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace CapstoneProject.Domain.Entities
{
    public class User : IdentityUser
    {
        public TechTrack TechTrack { get; set; }
        public ProgrammingLanguage MainProgrammingLanguage { get; set; }
        public Mentor Mentor { get; set; }
        public Mentee Mentee { get; set; }
    }
}
