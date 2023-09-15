using CapstoneProject.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CapstoneProject.Domain.Dtos.RequestDto
{
    public class UserRequestDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public UserType UserType { get; set; }
        [Required]
        public TechTrack TechTrack { get; set; }
        [Required]
        public ProgrammingLanguage ProgrammingLanguage { get; set; }
        [Required]
        public YearsOfExperience YearsOfExperience { get; set; }
        [Required]
        public MentorshipDuration MentorshipDuration { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
