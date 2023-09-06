using CapstoneProject.Domain.Dtos.RequestDto;
using CapstoneProject.Domain.Dtos.ResponseDto;
using CapstoneProject.Shared.RequestParameter.Common;
using CapstoneProject.Shared.RequestParameter.ModelParameters;

namespace CapstoneProject.Application.Services.Abstractions
{
    public interface IAppointmentScheduleService
    {
        Task<StandardResponse<AppointmentScheduleResponseDto>> CreateAppointmentScheduleAsync(AppointmentScheduleRequestDto appointmentScheduleRequest);
        Task<StandardResponse<IEnumerable<AppointmentScheduleResponseDto>>> GetAllSchedulesAsync();
        Task<StandardResponse<AppointmentScheduleResponseDto>> GetAppointmentScheduleByIdAsync(string id);
        Task<StandardResponse<AppointmentScheduleResponseDto>> DeleteAppointmentScheduleAsync(string id);
        Task<StandardResponse<AppointmentScheduleResponseDto>> UpdateAppointmentScheduleAsync(string id, AppointmentScheduleRequestDto appointmentScheduleRequest);
    }
}
