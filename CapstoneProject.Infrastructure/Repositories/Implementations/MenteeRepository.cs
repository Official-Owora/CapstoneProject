using CapstoneProject.Domain.Entities;
using CapstoneProject.Infrastructure.Persistence;
using CapstoneProject.Infrastructure.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CapstoneProject.Infrastructure.Repositories.Implementations
{
    public class MenteeRepository : RepositoryBase<Mentee>, IMenteeRepository
    {
        private readonly DbSet<Mentee> _mentees;

        public MenteeRepository(DataContext dataContext) : base(dataContext)
        {
            _mentees = dataContext.Set<Mentee>();
        }
       
        public async Task<IEnumerable<Mentee>> GetAllMenteesAsync()
        {
            var mentee = await _mentees.OrderBy(x => x.UserId).ToListAsync();
            return mentee;
        }


        public async Task<Mentee> GetMenteeByIdAsync(string id)
        {
            return await _mentees.Where(m => m.UserId.Equals(id)).FirstOrDefaultAsync();
        }
    }
}
