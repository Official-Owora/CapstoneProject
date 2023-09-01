using CapstoneProject.Domain.Dtos.RequestDto;
using CapstoneProject.Domain.Dtos.ResponseDto;
using Microsoft.AspNetCore.Identity;

namespace CapstoneProject.Application.Services.Abstractions
{
    public interface IAuthenticationService
    {
        Task<StandardResponse<IdentityResult>> RegisterUser(UserRequestDto userRequest);
        Task<bool> ValidateUser(UserLoginDto userForAuth);
        Task<string> CreateToken();
    }
}
