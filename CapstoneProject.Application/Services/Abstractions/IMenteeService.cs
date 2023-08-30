using CapstoneProject.Domain.Dtos.RequestDto;
using CapstoneProject.Domain.Dtos.ResponseDto;
using CapstoneProject.Shared.RequestParameter.Common;
using CapstoneProject.Shared.RequestParameter.ModelParameters;

namespace CapstoneProject.Application.Services.Abstractions
{
    public interface IMenteeService
    {
        Task<StandardResponse<MenteeResponseDto>> CreateMenteeAsync(MenteeRequestDto menteeRequest);
        Task<StandardResponse<(IEnumerable<MenteeResponseDto>, MetaData)>> GetAllMenteesAsync(MenteeRequestInputParameter parameter);
        Task<StandardResponse<MenteeResponseDto>> GetMenteeByIdAsync(int id);
        //Task<StandardResponse<(IEnumerable<MenteeResponseDto>, MetaData)>> GetMenteesByIsMatched(MenteeRequestInputParameter parameter, bool IsMatched);
        Task<StandardResponse<MenteeResponseDto>> DeleteMenteeAsync(int id);
        Task<StandardResponse<MenteeResponseDto>> UpdateMenteeAsync(int id, MenteeRequestDto menteeRequest);
    }
}
