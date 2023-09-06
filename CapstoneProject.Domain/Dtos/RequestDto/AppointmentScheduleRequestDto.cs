namespace CapstoneProject.Domain.Dtos.RequestDto
{
    public class AppointmentScheduleRequestDto
    {
        public string MentorId { get; set; }
        public DateTime AvailabilityDay { get; set; }
        public DateTime AvailabilityTime { get; set; }
        public DateTime AvailabilityStartTime { get; set; }
        public DateTime AvailabilityEndTime { get; set; }
    }
}
