using CapstoneProject.Domain.Entities;
using CapstoneProject.Infrastructure.Persistence;
using CapstoneProject.Infrastructure.Repositories.Abstractions;
using CapstoneProject.Shared.RequestParameter.Common;
using CapstoneProject.Shared.RequestParameter.ModelParameters;
using Microsoft.EntityFrameworkCore;

namespace CapstoneProject.Infrastructure.Repositories.Implementations
{
    internal sealed class MentorRepository : RepositoryBase<Mentor>, IMentorRepository
    {
        private readonly DbSet<Mentor> _mentors;

        public MentorRepository(DataContext dataContext) : base(dataContext)
        {
            _mentors = dataContext.Set<Mentor>();
        }
        public async Task<PagedList<Mentor>> GetAllMentorsPagedAsync()
        {
            var parameter = new MentorRequestInputParemeter();
            var mentors = await _mentors
                .OrderBy(x => x.UserId)
                .Where(m =>
                m.FirstName.ToLower().Contains(parameter.SearchTerm.ToLower()) ||
                m.LastName.ToLower().Contains(parameter.SearchTerm.ToLower()))
                .Skip((parameter.PageNumber - 1) * parameter.PageSize)
                .Take(parameter.PageSize).ToListAsync();
            var count = await _mentors.CountAsync();
            return new PagedList<Mentor>(mentors, count, parameter.PageNumber, parameter.PageSize);
        }
        public async Task<IEnumerable<Mentor>> GetAllMentorsAsync()
        {
            var mentor = await _mentors.OrderBy(x => x.UserId).ToListAsync();
            return mentor;
        }

           
        public async Task<Mentor> GetMentorByIdAsync(string id)
        {
            return await _mentors.Where(m => m.UserId.Equals(id)).FirstOrDefaultAsync();
        }
        public async Task<PagedList<Mentor>> GetMentorByIsAvailableAsync(MentorRequestInputParemeter parameter, bool isAvailable)
        {
            IQueryable<Mentor> mentors = _mentors;
            if (isAvailable)
            {
                mentors = mentors.Where(m => m.IsAvaiable);
            }
            var mentorsPage = await mentors
                .Skip((parameter.PageNumber - 1) * parameter.PageSize)
                .Take(parameter.PageSize).ToListAsync();
            var totalCount = await mentors.CountAsync();
            return new PagedList<Mentor>(mentorsPage, totalCount, parameter.PageNumber, parameter.PageSize);
        }
    }
}