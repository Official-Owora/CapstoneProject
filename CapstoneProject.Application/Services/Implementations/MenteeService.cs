using AutoMapper;
using CapstoneProject.Application.Services.Abstractions;
using CapstoneProject.Domain.Dtos.RequestDto;
using CapstoneProject.Domain.Dtos.ResponseDto;
using CapstoneProject.Domain.Entities;
using CapstoneProject.Infrastructure.RepositoryManager;
using CapstoneProject.Shared.RequestParameter.Common;
using CapstoneProject.Shared.RequestParameter.ModelParameters;
using Microsoft.Extensions.Logging;
using System.Diagnostics.Metrics;

namespace CapstoneProject.Application.Services.Implementations
{
    public class MenteeService : IMenteeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MenteeService> _logger;
        private readonly IMapper _mapper;

        public MenteeService(IUnitOfWork unitOfWork, ILogger<MenteeService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<StandardResponse<MenteeResponseDto>> CreateMenteeAsync(MenteeRequestDto menteeRequest)
        {
            try
            {
                if (menteeRequest == null)
                {
                    _logger.LogError("Mentee details cannot be null");
                    return StandardResponse<MenteeResponseDto>.Failed("Mentee request is nul");
                }
                _logger.LogInformation($"Trying to create a mentee : {DateTime.Now}");
                var mentee = _mapper.Map<Mentee>(menteeRequest);
                _logger.LogInformation($"Successfully created a mentee: {DateTime.Now}");
                await _unitOfWork.MenteeRepository.CreateAsync(mentee);
                await _unitOfWork.SaveAsync();
                _logger.LogInformation($"Successfully saved {mentee.UserId}");
                var menteeToReturn = _mapper.Map<MenteeResponseDto>(mentee);
                return StandardResponse<MenteeResponseDto>.Success($"Successfully created a mentee: {mentee.FirstName} {mentee.LastName}", menteeToReturn, 200);
            }
            catch (Exception ex)
            {

                return StandardResponse<MenteeResponseDto>.Failed($"Successfully created a mentee:{ex?.Message ?? ex?.InnerException.Message}");
            }
        }

        public async Task<StandardResponse<IEnumerable<MenteeResponseDto>>> GetAllMenteesAsync()
        {
            _logger.LogInformation("Attempting to get list of users from database.");
            var users = await _unitOfWork.MenteeRepository.GetAllMenteesAsync();
            var mapUsers = _mapper.Map<IEnumerable<MenteeResponseDto>>(users);
            _logger.LogInformation("Returning list of users.");
            return StandardResponse<IEnumerable<MenteeResponseDto>>.Success("successful", mapUsers, 200);
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
            /*var checkMenteeExists = await _unitOfWork.MenteeRepository.GetMenteeByIdAsync(id);
            if(checkMenteeExists == null)
            {
                _logger.LogError("Mentee does not exist");
                return StandardResponse<MenteeResponseDto>.Failed("Mentee cannot be found");
            }*/
            var mentors = await _unitOfWork.MentorRepository.GetAllMentorsAsync();
            Mentor mentor = null;
            foreach (var mentorDB in mentors)
            {
                if (mentorDB.TechTrack == menteeRequest.TechTrack && mentorDB.ProgrammingLanguage == menteeRequest.MainProgrammingLanguage && mentor == null)
                {
                    mentor=mentorDB;
                }
            }
            var mentee = _mapper.Map<Mentee>(menteeRequest);
            mentee.MentorId = mentor.UserId;
            _unitOfWork.MenteeRepository.Update(mentee);
            await _unitOfWork.SaveAsync();
            var menteeUpdated = _mapper.Map<MenteeResponseDto>(mentee);
            return StandardResponse<MenteeResponseDto>.Success($"Successfully updated a mentee with Id: {mentee.UserId}", menteeUpdated, 200);
        }
    }
}
