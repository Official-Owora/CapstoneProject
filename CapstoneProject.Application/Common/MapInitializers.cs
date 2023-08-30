using AutoMapper;
using CapstoneProject.Domain.Dtos.RequestDto;
using CapstoneProject.Domain.Dtos.ResponseDto;
using CapstoneProject.Domain.Entities;

namespace CapstoneProject.Application.Common
{
    public class MapInitializers : Profile
    {
        public MapInitializers()
        {
            CreateMap<UserRequestDto, User>();
            CreateMap<User, UserResponseDto>();
            CreateMap<MentorRequestDto, Mentor>();
            CreateMap<Mentor, MentorResponseDto>();
            CreateMap<MenteeRequestDto, Mentee>();
            CreateMap<Mentee, MenteeResponseDto>();
            CreateMap<AppointmentSchedule, AppointmentScheduleResponseDto>();
            CreateMap<AppointmentScheduleRequestDto, AppointmentSchedule>();
        }
    }
}
