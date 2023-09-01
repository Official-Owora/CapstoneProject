﻿using CapstoneProject.Domain.Enums;

namespace CapstoneProject.Domain.Dtos.RequestDto
{
    public class MenteeRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public TechTrack TechTrack { get; set; }
        public ProgrammingLanguage MainProgrammingLanguage { get; set; }
        public bool IsMatched { get; set; }
    }
}
