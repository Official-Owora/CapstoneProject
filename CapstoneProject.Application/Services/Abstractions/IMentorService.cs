using CapstoneProject.Domain.Dtos.RequestDto;
using CapstoneProject.Domain.Dtos.ResponseDto;
using Microsoft.AspNetCore.Http;

namespace CapstoneProject.Application.Services.Abstractions
{
    public interface IMentorService
    {
        
        Task<StandardResponse<IEnumerable<MentorResponseDto>>> GetAllMentorsAsync();
        Task<StandardResponse<MentorResponseDto>> GetMentorByIdAsync(string id);
        Task<StandardResponse<MentorResponseDto>> DeleteMentorAsync(string id);
        Task<StandardResponse<MentorResponseDto>> UpdateMentorAsync(string id, MentorRequestDto mentorRequest);
        Task<StandardResponse<(bool, string)>> UploadProfileImageAsync(string Id, IFormFile file);
    }
}
