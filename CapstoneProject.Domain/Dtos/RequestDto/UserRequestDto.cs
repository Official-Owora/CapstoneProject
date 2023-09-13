using CapstoneProject.Domain.Enums;

namespace CapstoneProject.Domain.Dtos.RequestDto
{
    public class UserRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserType Roles { get; set; }
        public TechTrack TechTrack { get; set; }
        public ProgrammingLanguage ProgrammingLanguage { get; set; }
        public int YearsOfExperience { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
