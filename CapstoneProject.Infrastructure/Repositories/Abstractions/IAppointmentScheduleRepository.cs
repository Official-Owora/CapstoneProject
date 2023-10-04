using CapstoneProject.Domain.Entities;
using CapstoneProject.Shared.RequestParameter.Common;
using CapstoneProject.Shared.RequestParameter.ModelParameters;

namespace CapstoneProject.Infrastructure.Repositories.Abstractions
{
    public interface IAppointmentScheduleRepository : IRepositoryBase<AppointmentSchedule>
    {
        Task<AppointmentSchedule> GetAppointmentScheduleByIdAsync(string id);
        Task<PagedList<AppointmentSchedule>> GetAllSchedulesAsync(AppointmentScheduleRequestInputParameter parameter);
    }
}
