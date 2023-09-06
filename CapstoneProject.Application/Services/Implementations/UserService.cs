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
        public async Task<StandardResponse<UserResponseDto>> CreateUserAsync(UserRequestDto userRequest)
        {
            if (userRequest == null)
            {
                _logger.LogError("User details cannot be found");
                return StandardResponse<UserResponseDto>.Failed($"User request is null");
            }
            _logger.LogInformation($"Attempting to create a user: {DateTime.Now}");
            var user = _mapper.Map<User>(userRequest);
            _logger.LogInformation($"User Successfully created: {DateTime.Now}");
            _unitOfWork.UserRepository.CreateAsync(user);
            await _unitOfWork.SaveAsync();
            _logger.LogInformation($"Successfully saved user with Id: {user.Id}");
            var userToReturn = _mapper.Map<UserResponseDto>(user);
            return StandardResponse<UserResponseDto>.Success($"Successfully created a user", userToReturn, 201);
        }
        public async Task<StandardResponse<(IEnumerable<UserResponseDto>, MetaData)>> GetAllUserAsync(UserRequestInputParameter parameter)
        {
            var result = await _unitOfWork.UserRepository.GetAllUsersAsync(parameter);
            var userToReturn = _mapper.Map<IEnumerable<UserResponseDto>>(result);
            return StandardResponse<(IEnumerable<UserResponseDto>, MetaData)>.Success("Successfully retrieved all users", (userToReturn, result.MetaData));
        }
        public async Task<StandardResponse<UserResponseDto>>GetUserById(string Id)
        {
            var result = await _unitOfWork.UserRepository.GetUserByIdAsync(Id);
            var userToReturn = _mapper.Map<UserResponseDto>(result);
            return StandardResponse<UserResponseDto>.Success("Successfully retrived the user", userToReturn);
        }
        public async Task<StandardResponse<UserResponseDto>> GetUserByEmail(string email)
        {
            var result = await _unitOfWork.UserRepository.GetUserByEmailAsync(email);
            var userToReturn = _mapper.Map<UserResponseDto>(result);
            return StandardResponse<UserResponseDto>.Success("Successfully retrieved a user", userToReturn);
        }
        public async Task<StandardResponse<UserResponseDto>> UpdateUserAsync(string id, UserRequestDto userRequest)
        {
            var userExists = await _unitOfWork.UserRepository.GetUserByIdAsync(id);
            if (userExists != null)
            {
                _logger.LogError("User not found");
                return StandardResponse<UserResponseDto>.Failed("User not found");
            }
            var user = _mapper.Map<User>(userRequest);
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveAsync();
            var userToReturn = _mapper.Map<UserResponseDto>(user);
            return StandardResponse<UserResponseDto>.Success($"Successfully updated a user: {user.UserName}", userToReturn, 200);

        }
        public async Task<StandardResponse<UserResponseDto>> DeleteUser(string id)
        {
            var response = new UserResponseDto();
            _logger.LogInformation($"Checking if the user with Id {id} exists");
            var getUser = await _unitOfWork.UserRepository.GetUserByIdAsync(id);
            if (getUser == null)
            {
                _logger.LogError("User not found");
                StandardResponse<UserResponseDto>.Failed("User does not exist");
            }
            _unitOfWork.UserRepository.Delete(getUser);
            _unitOfWork.SaveAsync();
            return StandardResponse<UserResponseDto>.Success($"User with Id {id}, has been deleted successfully", response);
        }

    }
}
