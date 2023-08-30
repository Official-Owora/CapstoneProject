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
            if (menteeRequest == null)
            {
                _logger.LogError("Mentee details cannot be null");
                return StandardResponse<MenteeResponseDto>.Failed("Mentee request is nul");
            }
            _logger.LogInformation($"Trying to create a mentee : {DateTime.Now}");
            var mentee = _mapper.Map<Mentee>(menteeRequest);
            _logger.LogInformation($"Successfully created a mentee: {DateTime.Now}");
            _unitOfWork.MenteeRepository.CreateAsync(mentee);
            await _unitOfWork.SaveAsync();
            _logger.LogInformation($"Successfully saved {mentee.Id}");
            var menteeToReturn = _mapper.Map<MenteeResponseDto>(mentee);
            return StandardResponse<MenteeResponseDto>.Success($"Successfully created a mentee: {mentee.FirstName } {mentee.LastName}", menteeToReturn, 200);
        }
        public async Task<StandardResponse<(IEnumerable<MenteeResponseDto>, MetaData)>> GetAllMenteesAsync(MenteeRequestInputParameter parameter)
        {
            var result = await _unitOfWork.MenteeRepository.GetAllMenteeAsync(parameter);
            var menteeToReturn = _mapper.Map<IEnumerable<MenteeResponseDto>>(result);
            return StandardResponse<(IEnumerable<MenteeResponseDto>, MetaData)>.Success("Successfully retrieved all mentees", (menteeToReturn, result.MetaData), 200);
        }
        public async Task<StandardResponse<MenteeResponseDto>> GetMenteeByIdAsync(int id)
        {
            var getMentee = await _unitOfWork.MenteeRepository.GetMenteeByIdAsync(id);
            var menteeToReturn = _mapper.Map<MenteeResponseDto>(getMentee);
            return StandardResponse<MenteeResponseDto>.Success($"Successfully retrieved a mentee with Id: {getMentee.Id}", menteeToReturn, 200);
        }
       /* public async Task<StandardResponse<(IEnumerable<MenteeResponseDto>, MetaData)>> GetMenteesByIsMatched(MenteeRequestInputParameter parameter, bool IsMatched)
        {
            var GetMentees = await _unitOfWork.MenteeRepository.GetMenteeByIsMatched(parameter, IsMatched);
            if (!IsMatched)
            {
                _logger.LogInformation("Mentees are still available to be matched");
                var matchedMentees = new List<MenteeResponseDto>();
                foreach (var mentee in GetMentees)
                {

                }
            }
        }*/
        public async Task<StandardResponse<MenteeResponseDto>> DeleteMenteeAsync(int id)
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
            return StandardResponse<MenteeResponseDto>.Success($"Successfully deleted mentee with Id {menteeToBeDelete.Id}", menteeToReturn, 200);
        }
        public async Task<StandardResponse<MenteeResponseDto>> UpdateMenteeAsync(int id, MenteeRequestDto menteeRequest)
        {
            var checkMenteeExists = await _unitOfWork.MenteeRepository.GetMenteeByIdAsync(id);
            if(checkMenteeExists == null)
            {
                _logger.LogError("Mentee does not exist");
                return StandardResponse<MenteeResponseDto>.Failed("Mentee cannot be found");
            }
            var mentee = _mapper.Map<Mentee>(menteeRequest);
            _unitOfWork.MenteeRepository.Update(mentee);
            await _unitOfWork.SaveAsync();
            var menteeUpdated = _mapper.Map<MenteeResponseDto>(mentee);
            return StandardResponse<MenteeResponseDto>.Success($"Successfully updated a mentee with Id: {mentee.Id}", menteeUpdated, 200);
        }
    }
}
