using CapstoneProject.Domain.Entities;
using CapstoneProject.Infrastructure.Persistence;
using CapstoneProject.Infrastructure.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CapstoneProject.Infrastructure.Repositories.Implementations
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly DbSet<User> _users;

        public UserRepository(DataContext dataContext) : base(dataContext)
        {
            _users = dataContext.Set<User>();
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var user = await _users.OrderBy(x => x.Id).ToListAsync();
            return user;
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
           return await _users.Where(u => u.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _users.Where(u => u.Email.Contains(email, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefaultAsync();
        }
        
    }
}
