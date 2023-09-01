using CapstoneProject.Domain.Common;
using CapstoneProject.Domain.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace CapstoneProject.Domain.Entities
{
    public class Mentor : BaseEntity
    {
        public string YearsOfExperience { get; set; }
        public string Organization { get; set; }
        public MentorshipDuration MentorshipDuration { get; set; }
        public bool IsAvaiable { get; set; }  //So mentor can update availability
        public ICollection<CommunicationChannels> communicationChannels;
        //Navigational property
        public ICollection<Mentee> mentees { get; set; }
        public ICollection<AppointmentSchedule> AppointmentSchedules { get; set; }
        [Key]
        public string UserId { get; set; }
        public User User { get; set; }

        
 

    }
}
