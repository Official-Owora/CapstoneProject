using CapstoneProject.Domain.Enums;
using System.ComponentModel;

namespace CapstoneProject.Domain.Dtos.RequestDto
{
    public class MentorRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public string UserId { get; set; }  
        public TechTrack TechTrack { get; set; }
        public ProgrammingLanguage ProgrammingLanguage { get; set; }
        //public string ImageURL { get; set; }
        public YearsOfExperience YearsOfExperience { get; set; }
        public string Organization { get; set; }
        public MentorshipDuration MentorshipDuration { get; set; }
        [Description("Update Availability by entering true or false")]
        public bool IsAvaiable { get; set; }
        public string CommunicationChannel { get; set; }
    }
}
