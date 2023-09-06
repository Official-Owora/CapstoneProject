using CapstoneProject.Domain.Dtos.RequestDto;
using CapstoneProject.Domain.Dtos.ResponseDto;
using CapstoneProject.Shared.RequestParameter.Common;
using CapstoneProject.Shared.RequestParameter.ModelParameters;

namespace CapstoneProject.Application.Services.Abstractions
{
    public interface IMenteeService
    {
        Task<StandardResponse<IEnumerable<MenteeResponseDto>>> GetAllMenteesAsync();
        Task<StandardResponse<MenteeResponseDto>> CreateMenteeAsync(MenteeRequestDto menteeRequest);
        //Task<StandardResponse<(IEnumerable<MenteeResponseDto>, MetaData)>> GetAllMenteesAsync();
        Task<StandardResponse<MenteeResponseDto>> GetMenteeByIdAsync(string id);
        //Task<StandardResponse<(IEnumerable<MenteeResponseDto>, MetaData)>> GetMenteesByIsMatched(MenteeRequestInputParameter parameter, bool IsMatched);
        Task<StandardResponse<MenteeResponseDto>> DeleteMenteeAsync(string id);
        Task<StandardResponse<MenteeResponseDto>> UpdateMenteeAsync(string id, MenteeRequestDto menteeRequest);
    }
}
