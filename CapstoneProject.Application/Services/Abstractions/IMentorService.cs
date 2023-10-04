using CapstoneProject.Domain.Dtos.RequestDto;
using CapstoneProject.Domain.Dtos.ResponseDto;
using CapstoneProject.Domain.Enums;
using CapstoneProject.Shared.RequestParameter.Common;
using CapstoneProject.Shared.RequestParameter.ModelParameters;
using Microsoft.AspNetCore.Http;

namespace CapstoneProject.Application.Services.Abstractions
{
    public interface IMentorService
    {
        Task<StandardResponse<PagedList<MentorResponseDto>>> GetAllMentorsAsync(MentorRequestInputParemeter paremeter);
        Task<StandardResponse<IEnumerable<MentorResponseDto>>> GetAllMentorsAsync();
        Task<StandardResponse<MentorResponseDto>> GetMentorByIdAsync(string id);
        Task<StandardResponse<MentorResponseDto>> DeleteMentorAsync(string id);
        Task<StandardResponse<MentorResponseDto>> UpdateMentorAsync(string id, MentorRequestDto mentorRequest);
        Task<StandardResponse<(bool, string)>> UploadProfileImageAsync(string Id, IFormFile file);
        Task<StandardResponse<IEnumerable<MentorResponseDto>>> GetMentorByOrganizationAsync(string organization);
        Task<StandardResponse<IEnumerable<MentorResponseDto>>> GetMentorByCommunicationChannelAsync(string communicationChannel);
        Task<StandardResponse<IEnumerable<MentorResponseDto>>> GetMentorByIsAvailableAsync(bool isAvailable);
       // Task<StandardResponse<IEnumerable<MentorResponseDto>>> GetListofAllMentorsAsync(int pageNumber);
       // Task<StandardResponse<MentorResponseDto>> GetById(string id);
        //Task<StandardResponse<MentorResponseDto>> GetMentorByIsAvailableAsync(bool IsAvailable);
    }
}
