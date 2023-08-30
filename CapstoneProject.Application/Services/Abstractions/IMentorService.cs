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
        Task<StandardResponse<MentorResponseDto>> GetMentorByIdAsync(int id);
        Task<StandardResponse<(IEnumerable<MentorResponseDto>, MetaData)>> GetMentorByIsAvailableAsync(MentorRequestInputParemeter paremeter, bool isAvailable);
        Task<StandardResponse<MentorResponseDto>> DeleteMentorAsync(int id);
        Task<StandardResponse<MentorResponseDto>> UpdateMentorAsync(int id, MentorRequestDto mentorRequest);
    }
}
