﻿using CapstoneProject.Domain.Common;
using CapstoneProject.Domain.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace CapstoneProject.Domain.Entities
{
    public class Mentor : BaseEntity
    {
        public string? Organization { get; set; }
        public bool IsAvaiable { get; set; } = true;  //So mentor can update availability
        public  string? CommunicationChannel { get; set; }
        //Navigational property
        public ICollection<Mentee> Mentees { get; set; }
        public ICollection<AppointmentSchedule> AppointmentSchedules { get; set; }
        [Key]
        public string? UserId { get; set; }
        public User? User { get; set; }    
    }
}
