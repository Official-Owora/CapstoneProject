using CapstoneProject.Domain.Dtos.ResponseDto;
using CapstoneProject.Domain.Entities;
using CapstoneProject.Shared.RequestParameter.Common;
using CapstoneProject.Shared.RequestParameter.ModelParameters;

namespace CapstoneProject.Infrastructure.Repositories.Abstractions
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<PagedList<User>> GetAllUsersAsync(UserRequestInputParameter parameter);
        Task<User> GetUserByIdAsync(string id);
        //void GetUserByIdAsync(string id);
        Task<User> GetUserByEmailAsync(string email);
       // Task<StandardResponse<UserResponseDto>> DeleteUser(string id);
    }
}
