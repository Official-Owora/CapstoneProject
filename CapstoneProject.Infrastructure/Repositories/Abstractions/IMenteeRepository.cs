using CapstoneProject.Domain.Entities;
using CapstoneProject.Domain.Enums;
using CapstoneProject.Shared.RequestParameter.Common;
using CapstoneProject.Shared.RequestParameter.ModelParameters;

namespace CapstoneProject.Infrastructure.Repositories.Abstractions
{
    public interface IMenteeRepository : IRepositoryBase<Mentee>
    {
        Task<IEnumerable<Mentee>> GetAllMenteesAsync();
        Task<Mentee> GetMenteeByIdAsync(string id);
    }
}
