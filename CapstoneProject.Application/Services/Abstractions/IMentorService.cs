using CapstoneProject.Domain.Dtos.RequestDto;
using CapstoneProject.Domain.Dtos.ResponseDto;

namespace CapstoneProject.Application.Services.Abstractions
{
    public interface IMentorService
    {
        Task<StandardResponse<MentorResponseDto>> CreateMentorAsync(MentorRequestDto mentorRequest);
        Task<StandardResponse<IEnumerable<MentorResponseDto>>> GetAllMentorsAsync();
        Task<StandardResponse<MentorResponseDto>> GetMentorByIdAsync(string id);
        Task<StandardResponse<MentorResponseDto>> DeleteMentorAsync(string id);
        Task<StandardResponse<MentorResponseDto>> UpdateMentorAsync(string id, MentorRequestDto mentorRequest);      
    }
}
