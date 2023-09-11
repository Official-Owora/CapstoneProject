using AutoMapper;
using CapstoneProject.Application.Services.Abstractions;
using CapstoneProject.Domain.Dtos.RequestDto;
using CapstoneProject.Domain.Dtos.ResponseDto;
using CapstoneProject.Domain.Entities;
using CapstoneProject.Domain.Enums;
using CapstoneProject.Infrastructure.RepositoryManager;
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

        public MentorService(IUnitOfWork unitOfWork, ILogger<MentorService> logger, IMapper mapper, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }
        
        public async Task<StandardResponse<IEnumerable<MentorResponseDto>>> GetAllMentorsAsync()
        {
            _logger.LogInformation("Attempting to get list of mentors from database.");
            var users = await _unitOfWork.MentorRepository.GetAllMentorsAsync();
            var mapUsers = _mapper.Map<IEnumerable<MentorResponseDto>>(users);
            _logger.LogInformation("Returning list of users.");
            return StandardResponse<IEnumerable<MentorResponseDto>>.Success("successful",mapUsers,200);
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
        /*public async Task<StandardResponse<(IEnumerable<MentorResponseDto>, MetaData)>> GetMentorByIsAvailableAsync(MentorRequestInputParemeter paremeter, bool isAvailable)
        {
            //Get all the available mentors
            var getByIsAvailable = await _unitOfWork.MentorRepository.GetMentorByIsAvailableAsync(paremeter, isAvailable);
            //Checking if mentors are available for matching
            if (isAvailable)
            {
                _logger.LogInformation("Mentors are still available to be matched");
                //creates a list to store the mentors that will be matched and iterates through each availavble mentor
                var matchedMentors = new List<MentorResponseDto>();
                foreach (var mentor in getByIsAvailable)
                {
                    //Trying to find a matching mentee based on common parameters
                    *//*var matchingMentee = await _unitOfWork.MenteeRepository.GetMenteeByIsMatched(mentor.Id, mentor.IsAvaiable, mentor.ProgrammingLanguage, mentor.TechTrack);
                    if (matchingMentee == null)
                    {
                        _logger.LogError("Mentee is null");

                        //return StandardResponse<(IEnumerable<MentorResponseDto>,MetaData)>.Failed("Mentor does not have a matching Mentee");
                        continue;
                    }
                    _logger.LogInformation($"Mentor {mentor.Id} matched with Mentee {matchingMentee.Id}");
                    //Marking the mentor and mentee as unavailable for further matching
                    matchingMentee.IsAvailable = false;
                    mentor.IsAvaiable = false;
                    //update mentor and mentee's availability 
                    _unitOfWork.MentorRepository.Update(mentor);
                    _unitOfWork.MenteeRepository.Update(matchingMentee);
                    //Add matched mentor to the list
                    matchedMentors.Add(_mapper.Map<MentorResponseDto>(mentor));*//*
                }
                if (matchedMentors.Count > 0)
                {
                    await _unitOfWork.SaveAsync();
                    return StandardResponse<(IEnumerable<MentorResponseDto>, MetaData)>.Failed("No matching mentee found for any mentor");
                }
                else
                {
                    return StandardResponse<(IEnumerable<MentorResponseDto>, MetaData)>.Success("successfully matched mentors with mentees", (null, null));
                }

            }
            return StandardResponse<(IEnumerable<MentorResponseDto>, MetaData)>.Success("Mentors are not available for matching.", (null, null));

        }*/
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
            if (!await _userManager.IsInRoleAsync(user, UserType.Mentor.ToString() ))
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

        //Endpoints to be created 

        //GetAvailableMentee     --->OrderByAscending
        //CreateAppointment Schedule
        //PairMentorWithMentee   ---->
        //Deactivate Account
    }
    
}
