using CapstoneProject.Domain.Entities;
using CapstoneProject.Infrastructure.Persistence;
using CapstoneProject.Infrastructure.Repositories.Abstractions;
using CapstoneProject.Shared.RequestParameter.Common;
using CapstoneProject.Shared.RequestParameter.ModelParameters;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace CapstoneProject.Infrastructure.Repositories.Implementations
{
    public class MenteeRepository : RepositoryBase<Mentee>, IMenteeRepository
    {
        private readonly DbSet<Mentee> _mentees;

        public MenteeRepository(DataContext dataContext) : base(dataContext)
        {
            _mentees = dataContext.Set<Mentee>();
        }
       
        public async Task<PagedList<Mentee>> GetAllMenteesAsync(MenteeRequestInputParameter parameter)
        {
            var mentee = _mentees.OrderBy(x => x.UserId);
            return await PagedList<Mentee>.GetPagination(mentee, parameter.PageNumber, parameter.PageSize);
        }

        public async Task<Mentee> GetMenteeByIdAsync(string id)
        {
            return await _mentees.Where(m => m.UserId.Equals(id)).FirstOrDefaultAsync();
        }
    }
}
