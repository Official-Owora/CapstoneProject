using AutoMapper;
using CapstoneProject.Application.Services.Abstractions;
using CapstoneProject.Domain.Dtos.RequestDto;
using CapstoneProject.Domain.Dtos.ResponseDto;
using CapstoneProject.Domain.Entities;
using CapstoneProject.Domain.Enums;
using CapstoneProject.Infrastructure.RepositoryManager;
using CapstoneProject.Shared.RequestParameter.ModelParameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace CapstoneProject.Application.Services.Implementations
{
    public class MentorService : IMentorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MentorService> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IPhotoService _photoService;

        public MentorService(IUnitOfWork unitOfWork, ILogger<MentorService> logger, IMapper mapper, UserManager<User> userManager, IPhotoService photoService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _photoService = photoService;
        }

        public async Task<StandardResponse<IEnumerable<MentorResponseDto>>> GetAllMentorsAsync(int pageNumber)
        {
            var parameter = new MenteeRequestInputParameter();
            parameter.PageNumber = pageNumber;
            parameter.PageSize = 2;

            _logger.LogInformation("Attempting to get list of mentors from database.");
            var users = await _unitOfWork.MentorRepository.GetAllMentorsAsync();
            var mapUsers = _mapper.Map<IEnumerable<MentorResponseDto>>(users);
            _logger.LogInformation("Returning list of users.");
            return StandardResponse<IEnumerable<MentorResponseDto>>.Success("successful",mapUsers,200);
        }
        public async Task<StandardResponse<IEnumerable<MentorResponseDto>>> GetAllMentorsAsync()
        {
            _logger.LogInformation("Attempting to get list of mentors from database.");
            var users = await _unitOfWork.MentorRepository.GetAllMentorsAsync();
            var mapUsers = _mapper.Map<IEnumerable<MentorResponseDto>>(users);
            _logger.LogInformation("Returning list of users.");
            return StandardResponse<IEnumerable<MentorResponseDto>>.Success("successful", mapUsers, 200);
        }
       
        public async Task<StandardResponse<MentorResponseDto>> GetMentorByIdAsync(string id)
        {
            var getMentorById = await _unitOfWork.MentorRepository.GetMentorByIdAsync(id);
            if (getMentorById == null)
            {
                _logger.LogError("Mentor not found");
                return StandardResponse<MentorResponseDto>.Failed("Mentor does not exist");
            }
            await _unitOfWork.MentorRepository.GetMentorByIdAsync(id);
            await _unitOfWork.SaveAsync();
            var mentorToReturn = _mapper.Map<MentorResponseDto>(getMentorById);
            return StandardResponse<MentorResponseDto>.Success($"Successfully retrieved a mentor with Id: {getMentorById.UserId}", mentorToReturn, 200);
        }
        
        public async Task<StandardResponse<MentorResponseDto>> DeleteMentorAsync(string id)
        {
            _logger.LogInformation($"Checking if the user with Id {id} exists");
            var mentorTobeDeleted = await _unitOfWork.MentorRepository.GetMentorByIdAsync(id);
            if(mentorTobeDeleted == null)
            {
                _logger.LogError("Mentor not found");
                return StandardResponse<MentorResponseDto>.Failed("Mentor does not exist");
            }
            _unitOfWork.MentorRepository.Delete(mentorTobeDeleted);
            await _unitOfWork.SaveAsync();
            var mentorToReturn = _mapper.Map<MentorResponseDto>(mentorTobeDeleted);
            return StandardResponse<MentorResponseDto>.Success($"Successfully deleted a mentor with Id: {mentorTobeDeleted.UserId}", mentorToReturn, 200);
        }
        public async Task<StandardResponse<MentorResponseDto>> UpdateMentorAsync(string id, MentorRequestDto mentorRequest)
        {
            var user = await _userManager.FindByIdAsync(id);
            
            if(user == null)
            {
                _logger.LogError("User does not exist");
                return StandardResponse<MentorResponseDto>.Failed("User does not exist");
            }
            if (!await _userManager.IsInRoleAsync(user, UserType.Mentor.ToString()))
            {
                _logger.LogError("User is not authorized to update a mentor profile");
                return StandardResponse<MentorResponseDto>.Failed("User is not authorized to update a mentor profile");
            }
            var checkMentorExists = await _unitOfWork.MentorRepository.GetMentorByIdAsync(id);
            if (checkMentorExists == null)
            {
                _logger.LogError("Mentor does not exist");
                return StandardResponse<MentorResponseDto>.Failed("Mentor does not exist");
            }
            
            var mentor = _mapper.Map<Mentor>(mentorRequest);
            _unitOfWork.MentorRepository.Update(mentor);
            await _unitOfWork.SaveAsync();
            var mentorUpdated = _mapper.Map<MentorResponseDto>(mentor);
            return StandardResponse<MentorResponseDto>.Success($"Successfully updated Mentor with Id: {mentor.UserId}", mentorUpdated, 200);
        }
        public async Task<StandardResponse<(bool, string)>> UploadProfileImageAsync(string Id, IFormFile file)
        {
            var result = await _unitOfWork.MentorRepository.GetMentorByIdAsync(Id);
            if (result is null)
            {
                _logger.LogWarning($"No mentor with id {Id}");
                return StandardResponse<(bool, string)>.Failed("No mentor found", 406);
            }
            var mentor = _mapper.Map<Mentor>(result);
            string url = _photoService.AddPhoto(file);
            if (string.IsNullOrWhiteSpace(url))
                return StandardResponse<(bool, string)>.Failed("Failed to upload", 500);
            mentor.ImageURL = url;
            _unitOfWork.MentorRepository.Update(mentor);
            await _unitOfWork.SaveAsync();
            return StandardResponse<(bool, string)>.Success("Successfully uploaded image", (true, url), 204);
        }
        /*public async Task<StandardResponse<MentorResponseDto>> GetMentorByIsAvailableAsync(bool IsAvailable)
        {
            var getMentorById = await _unitOfWork.MentorRepository.GetMentorByIdAsync(id);
            if (getMentorById == null)
            {
                _logger.LogError("Mentor not found");
                return StandardResponse<MentorResponseDto>.Failed("Mentor does not exist");
            }
            await _unitOfWork.MentorRepository.GetMentorByIdAsync(id);
            await _unitOfWork.SaveAsync();
            var mentorToReturn = _mapper.Map<MentorResponseDto>(getMentorById);
            return StandardResponse<MentorResponseDto>.Success($"Successfully retrieved a mentor with Id: {getMentorById.UserId}", mentorToReturn, 200);
        }*/

        //Endpoints to be created 

        //GetAvailableMentee     --->OrderByAscending
        //CreateAppointment Schedule
        //PairMentorWithMentee   ---->
        //Deactivate Account
    }
    
}
