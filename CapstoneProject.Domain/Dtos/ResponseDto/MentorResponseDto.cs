﻿using CapstoneProject.Domain.Enums;

namespace CapstoneProject.Domain.Dtos.ResponseDto
{
    public class MentorResponseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public TechTrack TechTrack { get; set; }
        public ProgrammingLanguage MainProgrammingLanguage { get; set; }
        public string ImageURL { get; set; }
        public string YearsOfExperience { get; set; }
        public string Organization { get; set; }
        public MentorshipDuration MentorshipDuration { get; set; }
        public bool IsAvaiable { get; set; }
        public ICollection<CommunicationChannels> communicationChannels;
    }
}