namespace CapstoneProject.Infrastructure.Repositories.Abstractions
{
    public interface IRepositoryBase<T>
    {
        Task CreateAsync(T entity);
        void Delete(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T entity);
    }
}
