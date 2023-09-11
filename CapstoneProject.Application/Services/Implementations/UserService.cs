using AutoMapper;
using CapstoneProject.Application.Services.Abstractions;
using CapstoneProject.Domain.Dtos.RequestDto;
using CapstoneProject.Domain.Dtos.ResponseDto;
using CapstoneProject.Domain.Entities;
using CapstoneProject.Infrastructure.RepositoryManager;
using CapstoneProject.Shared.RequestParameter.Common;
using CapstoneProject.Shared.RequestParameter.ModelParameters;
using Microsoft.Extensions.Logging;

namespace CapstoneProject.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        
        public async Task<StandardResponse<IEnumerable<UserResponseDto>>> GetAllUserAsync()
        {
            _logger.LogInformation("Attempting to get list of users from database.");
            var result = await _unitOfWork.UserRepository.GetAllUsersAsync();
            var userToReturn = _mapper.Map<IEnumerable<UserResponseDto>>(result);
            return StandardResponse<IEnumerable<UserResponseDto>>.Success("Successfully retrieved all users", userToReturn, 200);
        }
        public async Task<StandardResponse<UserResponseDto>>GetUserById(string Id)
        {
            var result = await _unitOfWork.UserRepository.GetUserByIdAsync(Id);
            var userToReturn = _mapper.Map<UserResponseDto>(result);
            return StandardResponse<UserResponseDto>.Success("Successfully retrived the user", userToReturn);
        }
        public async Task<StandardResponse<UserResponseDto>> GetUserByEmail(string email)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(email);
            if (user == null)
            {
                _logger.LogError("User does not exist");
                return StandardResponse<UserResponseDto>.Failed("User does not exist");
            }
            var userToReturn = _mapper.Map<UserResponseDto>(user);
            return StandardResponse<UserResponseDto>.Success("Successfully retrieved a user", userToReturn);
        }

        public async Task<StandardResponse<UserResponseDto>> DeleteUser(string id)
        {
            //var response = new UserResponseDto();
            _logger.LogInformation($"Checking if the user with Id {id} exists");
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                _logger.LogError("User not found");
                StandardResponse<UserResponseDto>.Failed("User does not exist");
            }            
            _unitOfWork.UserRepository.Delete(user);
            await _unitOfWork.SaveAsync();
            var userToReturn = _mapper.Map<UserResponseDto>(user);
            return StandardResponse<UserResponseDto>.Success($"User with Id {id}, has been deleted successfully", userToReturn, 200);
        }

    }
}
