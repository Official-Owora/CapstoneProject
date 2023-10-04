using AutoMapper;
using CapstoneProject.Application.Services.Abstractions;
using CapstoneProject.Domain.Dtos.RequestDto;
using CapstoneProject.Domain.Dtos.ResponseDto;
using CapstoneProject.Domain.Entities;
using CapstoneProject.Domain.Enums;
using CapstoneProject.Infrastructure.RepositoryManager;
using CapstoneProject.Shared.RequestParameter.Common;
using CapstoneProject.Shared.RequestParameter.ModelParameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata;

namespace CapstoneProject.Application.Services.Implementations
{
    public class MenteeService : IMenteeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MenteeService> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public MenteeService(IUnitOfWork unitOfWork, ILogger<MenteeService> logger, IMapper mapper, UserManager<User> userManager, IPhotoService photoService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _photoService = photoService;
        }

        public async Task<StandardResponse<PagedList<MenteeResponseDto>>> GetAllMenteesAsync(MenteeRequestInputParameter parameter)
        {
            _logger.LogInformation("Attempting to get list of users from database.");
            var users = await _unitOfWork.MenteeRepository.GetAllMenteesAsync(parameter);
            var mapUsers = _mapper.Map<IEnumerable<MenteeResponseDto>>(users);
            _logger.LogInformation("Returning list of users.");
            var pageList = new PagedList<MenteeResponseDto>(mapUsers.ToList(), users.MetaData.TotalCount, parameter.PageNumber, parameter.PageSize);
            return StandardResponse<PagedList<MenteeResponseDto>>.Success("successful", pageList, 200);
        }
        public async Task<StandardResponse<MenteeResponseDto>> GetMenteeByIdAsync(string id)
        {
            var getMentee = await _unitOfWork.MenteeRepository.GetMenteeByIdAsync(id);
            if (getMentee == null)
            {
                _logger.LogError("Mentee does not exist");
                return StandardResponse<MenteeResponseDto>.Failed($"Mentee with the id:{id} does not exist");
            }
            await _unitOfWork.MenteeRepository.GetMenteeByIdAsync(id);
            await _unitOfWork.SaveAsync();
            var menteeToReturn = _mapper.Map<MenteeResponseDto>(getMentee);
            return StandardResponse<MenteeResponseDto>.Success($"Successfully retrieved a mentee with Id: {getMentee.UserId}", menteeToReturn, 200);
        }

        public async Task<StandardResponse<MenteeResponseDto>> DeleteMenteeAsync(string id)
        {
            _logger.LogInformation($"Checking if the user with the Id {id} exists");
            var menteeToBeDelete = await _unitOfWork.MenteeRepository.GetMenteeByIdAsync(id);
            if (menteeToBeDelete == null)
            {
                _logger.LogError("Mentee does no exist");
                return StandardResponse<MenteeResponseDto>.Failed("Mentee cannot be found");
            }
            _unitOfWork.MenteeRepository.Delete(menteeToBeDelete);
            await _unitOfWork.SaveAsync();
            var menteeToReturn = _mapper.Map<MenteeResponseDto>(menteeToBeDelete);
            return StandardResponse<MenteeResponseDto>.Success($"Successfully deleted mentee with Id {menteeToBeDelete.UserId}", menteeToReturn, 200);
        }
        public async Task<StandardResponse<MenteeResponseDto>> UpdateMenteeAsync(string id, MenteeRequestDto menteeRequest)
        {
            var user = await  _userManager.FindByIdAsync(id);

            if (user == null)
            {
                _logger.LogError("User does not exist");
                return StandardResponse<MenteeResponseDto>.Failed("User does not exist");
            }
            if (!await _userManager.IsInRoleAsync(user, UserType.Mentee.ToString()))
            {
                _logger.LogError("User is not authorized to update a mentee profile");
                return StandardResponse<MenteeResponseDto>.Failed("User is not authorized to update a mentee profile");
            }
            var checkMenteeExists = await _unitOfWork.MenteeRepository.GetMenteeByIdAsync(id);
            if (checkMenteeExists == null)
            {
                _logger.LogError("Mentee does not exist");
                return StandardResponse<MenteeResponseDto>.Failed("Mentee does not exist");
            }
            /*var mentors = await _unitOfWork.MentorRepository.GetAllMentorsAsync();
            Mentor mentor = null;

            foreach (var mentorDB in mentors)
            {
                if (mentorDB.IsAvaiable == true && mentorDB.YearsOfExperience >= menteeRequest.YearsOfExperience && mentorDB.MentorshipDuration == menteeRequest.MentorshipDuration
                    && mentorDB.TechTrack == menteeRequest.TechTrack && mentorDB.ProgrammingLanguage == menteeRequest.ProgrammingLanguage && mentor == null)
                {
                    mentor = mentorDB;
                }
            }
            //Assigning the MentorId to the MentorId on the Mentee table. Recently added
            if (mentor != null)
            {
                //mentor.Mentees.Add(createMentee);
                checkMenteeExists.MentorId = mentor.UserId;
            }*/
            var mentee = _mapper.Map<Mentee>(menteeRequest);               
            _unitOfWork.MenteeRepository.Update(mentee);
            await _unitOfWork.SaveAsync();
            var menteeUpdated = _mapper.Map<MenteeResponseDto>(mentee);
            return StandardResponse<MenteeResponseDto>.Success($"Successfully updated Mentor with Id: {mentee.UserId}", menteeUpdated, 200);
        }
        public async Task<StandardResponse<(bool, string)>> UploadProfileImageAsync(string Id, IFormFile file)
        {
            var result = await _unitOfWork.MenteeRepository.GetMenteeByIdAsync(Id);
            if (result == null)
            {
                _logger.LogWarning($"No mentee with id {Id}");
                return StandardResponse<(bool, string)>.Failed("No mentee found", 406);
            }
            var mentee = _mapper.Map<Mentee>(result);
            string url = _photoService.AddPhoto(file);
            if (string.IsNullOrWhiteSpace(url))
                return StandardResponse<(bool, string)>.Failed("Failed to upload", 500);
            result.ImageURL = url;
            _unitOfWork.MenteeRepository.Update(result);
            await _unitOfWork.SaveAsync();
            return StandardResponse<(bool, string)>.Success("Successfully uploaded image", (false, url), 204);
        }
        
    }
}
