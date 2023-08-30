using CapstoneProject.Domain.Dtos.RequestDto;
using CapstoneProject.Domain.Dtos.ResponseDto;
using CapstoneProject.Shared.RequestParameter.Common;
using CapstoneProject.Shared.RequestParameter.ModelParameters;

namespace CapstoneProject.Application.Services.Abstractions
{
    public interface IAppointmentScheduleService
    {
        Task<StandardResponse<AppointmentScheduleResponseDto>> CreateAppointmentScheduleAsync(AppointmentScheduleRequestDto appointmentScheduleRequest);
        Task<StandardResponse<(IEnumerable<AppointmentScheduleResponseDto>, MetaData)>> GetAllAppointmentScheduleAsync(AppointmentScheduleRequestInputParameter parameter);
        Task<StandardResponse<AppointmentScheduleResponseDto>> GetAppointmentScheduleByIdAsync(int id);
        Task<StandardResponse<AppointmentScheduleResponseDto>> DeleteAppointmentScheduleAsync(int id);
        Task<StandardResponse<AppointmentScheduleResponseDto>> UpdateAppointmentScheduleAsync(int id, AppointmentScheduleRequestDto appointmentScheduleRequest);
    }
}
