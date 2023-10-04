using CapstoneProject.Domain.Entities;
using CapstoneProject.Infrastructure.Persistence;
using CapstoneProject.Infrastructure.Repositories.Abstractions;
using CapstoneProject.Shared.RequestParameter.Common;
using CapstoneProject.Shared.RequestParameter.ModelParameters;
using Microsoft.EntityFrameworkCore;

namespace CapstoneProject.Infrastructure.Repositories.Implementations
{
    public class MentorRepository : RepositoryBase<Mentor>, IMentorRepository
    {
        private readonly DbSet<Mentor> _mentors;

        public MentorRepository(DataContext dataContext) : base(dataContext)
        {
            _mentors = dataContext.Set<Mentor>();
        }

        public async Task<IEnumerable<Mentor>> GetAllMentorsAsync()
        {
            var mentor = await _mentors.OrderBy(x => x.UserId).ToListAsync();
            return mentor;

        }

        public async Task<PagedList<Mentor>> GetAllMentorsAsync(MentorRequestInputParemeter paremeter)
        {
            var mentors = _mentors.OrderBy(m => m.UserId);
            return await PagedList<Mentor>.GetPagination(mentors, paremeter.PageSize, paremeter.PageNumber);
        }

        public async Task<Mentor> GetMentorByIdAsync(string id)
        {
            return await _mentors.Where(m => m.UserId.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Mentor>> GetMentorByIsAvailableAsync(bool isAvailable)
        {
            var result = await _mentors.Where(m => m.IsAvaiable == true || false).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Mentor>> GetMentorByOrganizationAsync(string organization)
        {
            var mentor = await _mentors.Where(m => m.Organization == organization).ToListAsync();
            return mentor;
        }

        public async Task<IEnumerable<Mentor>> GetMentorByCommunicationChannelAsync(string communicationChannel)
        {
            var mentor = await _mentors.Where(m => m.CommunicationChannel == communicationChannel).ToListAsync();
            return mentor;
        }            
       
    }
}