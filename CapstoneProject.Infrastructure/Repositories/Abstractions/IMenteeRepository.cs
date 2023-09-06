using CapstoneProject.Domain.Entities;
using CapstoneProject.Domain.Enums;
using CapstoneProject.Shared.RequestParameter.Common;
using CapstoneProject.Shared.RequestParameter.ModelParameters;

namespace CapstoneProject.Infrastructure.Repositories.Abstractions
{
    public interface IMenteeRepository : IRepositoryBase<Mentee>
    {
        Task<PagedList<Mentee>> GetAllMenteeAsync();
        Task<Mentee> GetMenteeByIdAsync(string id);
        Task<PagedList<Mentee>> GetMenteeByIsMatched(MenteeRequestInputParameter parameter, bool IsMatched, ProgrammingLanguage programmingLanguage, TechTrack techTrack);
    }
}
