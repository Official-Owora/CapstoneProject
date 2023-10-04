using CapstoneProject.Domain.Entities;
using CapstoneProject.Infrastructure.Persistence;
using CapstoneProject.Infrastructure.Repositories.Abstractions;
using CapstoneProject.Shared.RequestParameter.Common;
using CapstoneProject.Shared.RequestParameter.ModelParameters;
using Microsoft.EntityFrameworkCore;

namespace CapstoneProject.Infrastructure.Repositories.Implementations
{
    public class AppointmentScheduleRepository : RepositoryBase<AppointmentSchedule>, IAppointmentScheduleRepository
    {
        private readonly DbSet<AppointmentSchedule> _schedules;

        public AppointmentScheduleRepository(DataContext dataContext) : base(dataContext)
        {
            _schedules = dataContext.Set<AppointmentSchedule>();
        }
        
        public async Task<AppointmentSchedule> GetAppointmentScheduleByIdAsync(string id)
        {
             return await _schedules.Where(a => a.Id.Equals(id)).FirstOrDefaultAsync();
        }
        public async Task<PagedList<AppointmentSchedule>> GetAllSchedulesAsync(AppointmentScheduleRequestInputParameter parameter)
        {
            var appointment = _schedules.OrderBy(x => x.Id);
            return await PagedList<AppointmentSchedule>.GetPagination(appointment, parameter.PageNumber, parameter.PageSize); ;
        }
    }
}