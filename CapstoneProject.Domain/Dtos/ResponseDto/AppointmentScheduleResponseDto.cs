namespace CapstoneProject.Domain.Dtos.ResponseDto
{
    public class AppointmentScheduleResponseDto
    {
        public DateTime AvailabilityDay { get; set; }
        public DateTime AvailabilityTime { get; set; }
        public DateTime AvailabilityStartTime { get; set; }
        public DateTime AvailabilityEndTime { get; set; }
    }
}
