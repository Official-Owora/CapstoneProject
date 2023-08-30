using CapstoneProject.Domain.Dtos.RequestDto;
using Microsoft.AspNetCore.Identity;

namespace CapstoneProject.Application.Services.Abstractions
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUser(UserRequestDto userRequest);
        Task<bool> ValidateUser(UserLoginDto userForAuth);
        Task<string> CreateToken();
    }
}
