using CapstoneProject.Application.Services.Abstractions;
using CapstoneProject.Domain.Dtos.RequestDto;
using CapstoneProject.Shared.RequestParameter.ModelParameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CapstoneProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentScheduleController : ControllerBase
    {
        private readonly IAppointmentScheduleService _appointmentScheduleService;

        public AppointmentScheduleController(IAppointmentScheduleService appointmentScheduleService)
        {
            _appointmentScheduleService = appointmentScheduleService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateAppointmentScheduleAsync([FromBody] AppointmentScheduleRequestDto appointmentScheduleRequest)
        {
            var result = await _appointmentScheduleService.CreateAppointmentScheduleAsync(appointmentScheduleRequest);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAppointmentSchedulesAsync()
        {
            var result = await _appointmentScheduleService.GetAllSchedulesAsync();
            return Ok(result);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointmentScheduleAsync(string id)
        {
            var result = await _appointmentScheduleService.GetAppointmentScheduleByIdAsync(id);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointmentScheduleAsync(string id)
        {
            var result = await _appointmentScheduleService.GetAppointmentScheduleByIdAsync(id);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointmentScheduleAsync(string id, [FromBody] AppointmentScheduleRequestDto appointmentScheduleRequest)
        {
            var result = await _appointmentScheduleService.UpdateAppointmentScheduleAsync(id, appointmentScheduleRequest);
            return Ok(result);
        }
    }
   
}
