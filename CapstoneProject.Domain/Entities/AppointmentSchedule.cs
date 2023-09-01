using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapstoneProject.Domain.Entities
{
    public class AppointmentSchedule
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString().Substring(0,7);
        public DateTime AvailabilityDay { get; set; }
        public DateTime AvailabilityTime { get; set; }
        public DateTime AvailabilityStartTime { get; set; }
        public DateTime AvailabilityEndTime { get; set; }
        [ForeignKey(nameof(Mentor))]
        public string MentorId { get; set; }
        public Mentor Mentor { get; set; }
    }
}
