using CapstoneProject.Domain.Entities;
using CapstoneProject.Shared.RequestParameter.Common;
using CapstoneProject.Shared.RequestParameter.ModelParameters;

namespace CapstoneProject.Infrastructure.Repositories.Abstractions
{
    public interface IMentorRepository : IRepositoryBase<Mentor>
    {
        Task<PagedList<Mentor>> GetAllMentorAsync(MentorRequestInputParemeter parameter);
        Task<Mentor> GetMentorByIdAsync(int id);
        Task<PagedList<Mentor>> GetMentorByIsAvailableAsync(MentorRequestInputParemeter parameter, bool isAvailable);
    }
}
