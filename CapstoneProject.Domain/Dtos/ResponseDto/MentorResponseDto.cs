using CapstoneProject.Domain.Enums;

namespace CapstoneProject.Domain.Dtos.ResponseDto
{
    public class MentorResponseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public TechTrack TechTrack { get; set; }
        public string ImageURL { get; set; }
        public YearsOfExperience YearsOfExperience { get; set; }
        public string Organization { get; set; }
        public MentorshipDuration MentorshipDuration { get; set; }
        public bool IsAvaiable { get; set; }
        //public string UserId { get; set; }
        public string CommunicationChannel { get; set; }
    }
}
