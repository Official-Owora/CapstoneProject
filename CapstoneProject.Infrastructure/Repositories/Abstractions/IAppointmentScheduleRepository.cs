using CapstoneProject.Domain.Entities;
using CapstoneProject.Shared.RequestParameter.Common;
using CapstoneProject.Shared.RequestParameter.ModelParameters;

namespace CapstoneProject.Infrastructure.Repositories.Abstractions
{
    public interface IAppointmentScheduleRepository : IRepositoryBase<AppointmentSchedule>
    {
        Task<PagedList<AppointmentSchedule>> GetAllAppointmentScheduleAsync(AppointmentScheduleRequestInputParameter parameter);
        Task<AppointmentSchedule> GetAppointmentScheduleByIdAsync(int id);
    }
}
