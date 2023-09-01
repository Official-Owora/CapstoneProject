using CapstoneProject.Domain.Dtos.RequestDto;
using CapstoneProject.Domain.Dtos.ResponseDto;
using CapstoneProject.Shared.RequestParameter.Common;
using CapstoneProject.Shared.RequestParameter.ModelParameters;

namespace CapstoneProject.Application.Services.Abstractions
{
    public interface IMentorService
    {
        Task<StandardResponse<MentorResponseDto>> CreateMentorAsync(MentorRequestDto mentorRequest);
        Task<StandardResponse<(IEnumerable<MentorResponseDto>, MetaData)>> GetAllMentorsAsync(MentorRequestInputParemeter paremeter);
        Task<StandardResponse<MentorResponseDto>> GetMentorByIdAsync(string id);
        //Task<StandardResponse<(IEnumerable<MentorResponseDto>, MetaData)>> GetMentorByIsAvailableAsync(MentorRequestInputParemeter paremeter, bool isAvailable);
        Task<StandardResponse<MentorResponseDto>> DeleteMentorAsync(string id);
        Task<StandardResponse<MentorResponseDto>> UpdateMentorAsync(string id, MentorRequestDto mentorRequest);
    }
}
