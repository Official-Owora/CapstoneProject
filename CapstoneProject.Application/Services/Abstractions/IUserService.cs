using CapstoneProject.Domain.Dtos.RequestDto;
using CapstoneProject.Domain.Dtos.ResponseDto;

namespace CapstoneProject.Application.Services.Abstractions
{
    public interface IUserService
    {
        Task<StandardResponse<UserResponseDto>> CreateUserAsync(UserRequestDto userRequest);
    }
}
