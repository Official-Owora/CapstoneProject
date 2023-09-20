using System.Linq.Expressions;

namespace CapstoneProject.Infrastructure.Repositories.Abstractions
{
    public interface IRepositoryBase<T>
    {
        Task CreateAsync(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
