using AutoMapper;
using CapstoneProject.Application.Services.Abstractions;
using CapstoneProject.Domain.Dtos.RequestDto;
using CapstoneProject.Domain.Dtos.ResponseDto;
using CapstoneProject.Domain.Entities;
using CapstoneProject.Infrastructure.RepositoryManager;
using CapstoneProject.Shared.RequestParameter.Common;
using CapstoneProject.Shared.RequestParameter.ModelParameters;
using Microsoft.Extensions.Logging;
using System.Security.AccessControl;

namespace CapstoneProject.Application.Services.Implementations
{
    public class MentorService : IMentorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MentorService> _logger;
        private readonly IMapper _mapper;

        public MentorService(IUnitOfWork unitOfWork, ILogger<MentorService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<StandardResponse<MentorResponseDto>> CreateMentorAsync(MentorRequestDto mentorRequest)
        {
            if (mentorRequest == null)
            {
                _logger.LogError("mentor details cannot be null");
                return StandardResponse<MentorResponseDto>.Failed("Mentor request is null");
            }
            _logger.LogInformation($"Trying to create a Mentor: {DateTime.Now}");
            var mentor = _mapper.Map<Mentor>(mentorRequest);
            _logger.LogInformation($"Successfully created a mentor: {DateTime.Now}");
            _unitOfWork.MentorRepository.CreateAsync(mentor);
            //Saving to Database
            await _unitOfWork.SaveAsync();
            _logger.LogInformation($"Successfully saved {mentor.FirstName} {mentor.LastName}");
            //mapping the saved data (source) to frontend
            var mentorToReturn = _mapper.Map<MentorResponseDto>(mentor);
            return StandardResponse<MentorResponseDto>.Success($"Successfully created a Mentor: {mentorRequest.FirstName} {mentorRequest.LastName}", mentorToReturn, 200);
        }
        public async Task<StandardResponse<(IEnumerable<MentorResponseDto>, MetaData)>> GetAllMentorsAsync(MentorRequestInputParemeter paremeter)
        {
            var result = await _unitOfWork.MentorRepository.GetAllMentorAsync(paremeter);
            var mentorToReturn = _mapper.Map<IEnumerable<MentorResponseDto>>(result);
            return StandardResponse<(IEnumerable<MentorResponseDto>, MetaData)>.Success("All Mentors successfully retrieved", (mentorToReturn, result.MetaData), 200);
        }
        public async Task<StandardResponse<MentorResponseDto>> GetMentorByIdAsync(int id)
        {
            var getMentorById = await _unitOfWork.MentorRepository.GetMentorByIdAsync(id);
            var mentorToReturn = _mapper.Map<MentorResponseDto>(getMentorById);
            return StandardResponse<MentorResponseDto>.Success($"Successfully retrieved a mentor with {getMentorById.Id} Id", mentorToReturn, 200);
        }
        public async Task<StandardResponse<(IEnumerable<MentorResponseDto>, MetaData)>> GetMentorByIsAvailableAsync(MentorRequestInputParemeter paremeter, bool isAvailable)
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
                    /*var matchingMentee = await _unitOfWork.MenteeRepository.GetMenteeByIsMatched(mentor.Id, mentor.IsAvaiable, mentor.ProgrammingLanguage, mentor.TechTrack);
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
                    matchedMentors.Add(_mapper.Map<MentorResponseDto>(mentor));*/
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

        }
        public async Task<StandardResponse<MentorResponseDto>> DeleteMentorAsync(int id)
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
            return StandardResponse<MentorResponseDto>.Success($"Successfully deleted a mentor with Id: {mentorTobeDeleted.Id}", mentorToReturn, 200);
        }
        public async Task<StandardResponse<MentorResponseDto>> UpdateMentorAsync(int id, MentorRequestDto mentorRequest)
        {
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
            return StandardResponse<MentorResponseDto>.Success($"Successfully updated Mentor with Id: {mentor.Id}", mentorUpdated, 200);
        }
    }
    
}
