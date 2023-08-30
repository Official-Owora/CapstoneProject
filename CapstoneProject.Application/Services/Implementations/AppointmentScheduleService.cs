﻿using AutoMapper;
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
    public class AppointmentScheduleService : IAppointmentScheduleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AppointmentScheduleService> _logger;
        private readonly IMapper _mapper;

        public AppointmentScheduleService(IUnitOfWork unitOfWork, ILogger<AppointmentScheduleService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<StandardResponse<AppointmentScheduleResponseDto>> CreateAppointmentScheduleAsync(AppointmentScheduleRequestDto appointmentScheduleRequest)
        {
            if (appointmentScheduleRequest == null)
            {
                _logger.LogError("Appointment Schedule cannot be null");
                return StandardResponse<AppointmentScheduleResponseDto>.Failed("Appointment Schedule cannot be null");
            }
            _logger.LogInformation($"Trying to create an Appointment Schedule {DateTime.Now}");
            var appointmentSchedule = _mapper.Map<AppointmentSchedule>(appointmentScheduleRequest);
            _logger.LogInformation($"Successfully created an appointment Schedule: {DateTime.Now}");
            _unitOfWork.AppointmentScheduleRepository.CreateAsync(appointmentSchedule);
            await _unitOfWork.SaveAsync();
            _logger.LogInformation($"Successfully saved an appointment {appointmentSchedule.Id}");
            var scheduleToReturn = _mapper.Map<AppointmentScheduleResponseDto>(appointmentSchedule);
            return StandardResponse<AppointmentScheduleResponseDto>.Success($"Successfully created an appointment Schedule: {appointmentSchedule.Id}",scheduleToReturn, 200);
        }
        public async Task<StandardResponse<(IEnumerable<AppointmentScheduleResponseDto>, MetaData)>> GetAllAppointmentScheduleAsync(AppointmentScheduleRequestInputParameter parameter)
        {
            var result = await _unitOfWork.AppointmentScheduleRepository.GetAllAppointmentScheduleAsync(parameter);
            var scheduleToReturn = _mapper.Map<IEnumerable<AppointmentScheduleResponseDto>>(result);
            return StandardResponse<(IEnumerable<AppointmentScheduleResponseDto>, MetaData)>.Success($"All Appointment Schedules successfully retrieved", (scheduleToReturn, result.MetaData), 200);
        }
        public async Task<StandardResponse<AppointmentScheduleResponseDto>> GetAppointmentScheduleByIdAsync(int id)
        {
            var getSchedule = _unitOfWork.AppointmentScheduleRepository.GetAppointmentScheduleByIdAsync(id);
            var scheduleToReturn = _mapper.Map<AppointmentScheduleResponseDto>(getSchedule);
            return StandardResponse<AppointmentScheduleResponseDto>.Success($"Successfully retrieved appointment schedule with Id: {getSchedule.Id}",scheduleToReturn, 200);
        }
        public async Task<StandardResponse<AppointmentScheduleResponseDto>> DeleteAppointmentScheduleAsync(int id)
        {
            _logger.LogInformation($"Checking if the appointment schedule with Id {id} exists");
            var getSchedule = await _unitOfWork.AppointmentScheduleRepository.GetAppointmentScheduleByIdAsync(id);
            if (getSchedule == null)
            {
                _logger.LogError("Appointment Schedule not found");
                return StandardResponse<AppointmentScheduleResponseDto>.Failed("Appointment Schedule does not exist");
            }
            _unitOfWork.AppointmentScheduleRepository.Delete(getSchedule);
            await _unitOfWork.SaveAsync();
            var scheduleToReturn = _mapper.Map<AppointmentScheduleResponseDto>(getSchedule);
            return StandardResponse<AppointmentScheduleResponseDto>.Success($"Successfully deleted an appointment Schedule with Id: {getSchedule.Id}", scheduleToReturn, 200);
        }
        public async Task<StandardResponse<AppointmentScheduleResponseDto>> UpdateAppointmentScheduleAsync(int id, AppointmentScheduleRequestDto appointmentScheduleRequest)
        {
            _logger.LogInformation($"Checking if the appointment schedule with Id: {id} exists");
            var CheckAppointmentSchedule = await _unitOfWork.AppointmentScheduleRepository.GetAppointmentScheduleByIdAsync(id);
            if (CheckAppointmentSchedule == null)
            {
                _logger.LogError("Appointment Schedule Does not exist");
                return StandardResponse<AppointmentScheduleResponseDto>.Failed("Appointment Schedule does not exist");
            }
            var appointmentSchedule = _mapper.Map<AppointmentSchedule>(appointmentScheduleRequest);
            _unitOfWork.AppointmentScheduleRepository.Update(appointmentSchedule);
            await _unitOfWork.SaveAsync();
            var appointmentScheduleUpdate = _mapper.Map<AppointmentScheduleResponseDto>(appointmentSchedule);
            return StandardResponse<AppointmentScheduleResponseDto>.Success($"Successfully updated an appointment schedule with Id: {appointmentSchedule.Id}", appointmentScheduleUpdate, 200);
        }
    }
}
