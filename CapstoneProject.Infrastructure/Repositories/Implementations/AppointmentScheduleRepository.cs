using CapstoneProject.Domain.Entities;
using CapstoneProject.Infrastructure.Persistence;
using CapstoneProject.Infrastructure.Repositories.Abstractions;
using CapstoneProject.Shared.RequestParameter.Common;
using CapstoneProject.Shared.RequestParameter.ModelParameters;
using Microsoft.EntityFrameworkCore;

namespace CapstoneProject.Infrastructure.Repositories.Implementations
{
    internal sealed class AppointmentScheduleRepository : RepositoryBase<AppointmentSchedule>, IAppointmentScheduleRepository
    {
        private readonly DbSet<AppointmentSchedule> _schedules;

        public AppointmentScheduleRepository(DataContext dataContext) : base(dataContext)
        {
            _schedules = dataContext.Set<AppointmentSchedule>();
        }
        public async Task<PagedList<AppointmentSchedule>> GetAllAppointmentScheduleAsync(AppointmentScheduleRequestInputParameter parameter)
        {
            var schedules = await _schedules.Where(m => m.AvailabilityDay.Date == parameter.SearchDate.Date)
                .Skip((parameter.PageNumber - 1) * parameter.PageSize)
                .Take(parameter.PageSize).ToListAsync();
            var count = await _schedules.CountAsync();
            return new PagedList<AppointmentSchedule>(schedules, count, parameter.PageNumber, parameter.PageSize);
        }
        public async Task<AppointmentSchedule> GetAppointmentScheduleByIdAsync(int id)
        {
            return await _schedules.Where(a => a.Id.Equals(id)).FirstOrDefaultAsync();
        }

    }
}
