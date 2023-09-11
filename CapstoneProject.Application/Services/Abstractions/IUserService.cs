using CapstoneProject.Domain.Dtos.RequestDto;
using CapstoneProject.Domain.Dtos.ResponseDto;
using CapstoneProject.Shared.RequestParameter.Common;
using CapstoneProject.Shared.RequestParameter.ModelParameters;

namespace CapstoneProject.Application.Services.Abstractions
{
    public interface IUserService
    {
        Task<StandardResponse<IEnumerable<UserResponseDto>>> GetAllUserAsync();
        Task<StandardResponse<UserResponseDto>> GetUserById(string Id);
        Task<StandardResponse<UserResponseDto>> GetUserByEmail(string email);
        Task<StandardResponse<UserResponseDto>> DeleteUser(string id);
    }
}
