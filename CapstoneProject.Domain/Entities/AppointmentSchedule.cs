using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapstoneProject.Domain.Entities
{
    public class AppointmentSchedule
    {
        [Key]
        public int Id { get; set; }
        public DateTime AvailabilityDay { get; set; }
        public DateTime AvailabilityTime { get; set; }
        public DateTime AvailabilityStartTime { get; set; }
        public DateTime AvailabilityEndTime { get; set; }
        [ForeignKey(nameof(Mentor))]
        public int MentorId { get; set; }
        public Mentor Mentor { get; set; }
    }
}
