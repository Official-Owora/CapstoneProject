using CapstoneProject.Domain.Dtos.RequestDto;
using CapstoneProject.Domain.Dtos.ResponseDto;
using CapstoneProject.Shared.RequestParameter.Common;
using CapstoneProject.Shared.RequestParameter.ModelParameters;
using Microsoft.AspNetCore.Http;

namespace CapstoneProject.Application.Services.Abstractions
{
    public interface IMenteeService
    {
        Task<StandardResponse<PagedList<MenteeResponseDto>>> GetAllMenteesAsync(MenteeRequestInputParameter parameter);
        Task<StandardResponse<MenteeResponseDto>> GetMenteeByIdAsync(string id);
        Task<StandardResponse<MenteeResponseDto>> DeleteMenteeAsync(string id);
        Task<StandardResponse<MenteeResponseDto>> UpdateMenteeAsync(string id, MenteeRequestDto menteeRequest);
        Task<StandardResponse<(bool, string)>> UploadProfileImageAsync(string Id, IFormFile file);
    }
}
