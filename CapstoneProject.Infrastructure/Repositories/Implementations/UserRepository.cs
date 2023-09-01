using CapstoneProject.Domain.Entities;
using CapstoneProject.Infrastructure.Persistence;
using CapstoneProject.Infrastructure.Repositories.Abstractions;
using CapstoneProject.Shared.RequestParameter.Common;
using CapstoneProject.Shared.RequestParameter.ModelParameters;
using Microsoft.EntityFrameworkCore;

namespace CapstoneProject.Infrastructure.Repositories.Implementations
{
    internal sealed class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly DbSet<User> _users;

        public UserRepository(DataContext dataContext) : base(dataContext)
        {
            _users = dataContext.Set<User>();
        }
        public async Task<PagedList<User>> GetAllUsersAsync(UserRequestInputParameter parameter)
        {
            var users = await _users.Where(u => u.Email.ToLower().Contains(parameter.SearchTerm.ToLower()))
                .Skip((parameter.PageNumber - 1) * parameter.PageSize)
                .Take(parameter.PageSize).ToListAsync();
            var count = await _users.CountAsync();
            return new PagedList<User>(users, count, parameter.PageNumber, parameter.PageSize);
        }
        public async Task<User> GetUserByIdAsync(string id)
        {
            return await _users.Where(u => u.Id.Equals(id)).FirstOrDefaultAsync();
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _users.Where(u => u.Email.Contains(email, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefaultAsync();
        }
        public async Task<User> DeleteAsync(string id)
        {
            return await _users.FindAsync(id);
        }
    }
}
