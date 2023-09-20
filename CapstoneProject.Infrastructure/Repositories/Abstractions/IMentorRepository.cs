using CapstoneProject.Domain.Entities;
using CapstoneProject.Domain.Enums;
using CapstoneProject.Shared.RequestParameter.Common;
using CapstoneProject.Shared.RequestParameter.ModelParameters;

namespace CapstoneProject.Infrastructure.Repositories.Abstractions
{
    public interface IMentorRepository : IRepositoryBase<Mentor>
    {
        Task<PagedList<Mentor>> GetAllMentorsAsync(MentorRequestInputParemeter paremeter);
        Task<IEnumerable<Mentor>> GetAllMentorsAsync();
        Task<Mentor> GetMentorByIdAsync(string id);
        Task<IEnumerable<Mentor>> GetMentorByIsAvailableAsync(bool isAvailable);
        Task<IEnumerable<Mentor>> GetMentorByOrganizationAsync(string organization);
        Task<IEnumerable<Mentor>> GetMentorByCommunicationChannelAsync(string communicationChannel);
        
    }
}
