﻿using CapstoneProject.Domain.Enums;

namespace CapstoneProject.Domain.Dtos.ResponseDto
{
    public class MenteeResponseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public string ImageURL { get; set; }
        public TechTrack TechTrack { get; set; }
        public MentorshipDuration MentorshipDuration { get; set;}
        public YearsOfExperience YearsOfExperience { get; set; }
        //public string UserId { get; set; }
        //public string MentorId { get; set; }
    }
}
