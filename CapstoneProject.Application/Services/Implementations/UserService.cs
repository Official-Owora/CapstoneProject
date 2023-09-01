using AutoMapper;
using CapstoneProject.Application.Services.Abstractions;
using CapstoneProject.Domain.Dtos.RequestDto;
using CapstoneProject.Domain.Dtos.ResponseDto;
using CapstoneProject.Domain.Entities;
using CapstoneProject.Infrastructure.RepositoryManager;
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

    }
}
