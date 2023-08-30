using CapstoneProject.Infrastructure.Persistence;
using CapstoneProject.Infrastructure.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CapstoneProject.Infrastructure.Repositories.Implementations
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DbSet<T> _dataContext;
        public RepositoryBase(DataContext dataContext)
        {
            _dataContext = dataContext.Set<T>();
        }

        public async Task CreateAsync(T entity) => await _dataContext.AddAsync(entity);
        public void Delete(T entity) => _dataContext.Remove(entity);
        public void RemoveRange(IEnumerable<T> entities) => _dataContext.RemoveRange(entities);
        public void Update(T entity) => _dataContext.Update(entity);
    }
}
