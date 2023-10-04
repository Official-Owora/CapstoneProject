using CapstoneProject.Application.Services.Abstractions;
using CapstoneProject.Domain.Dtos.RequestDto;
using CapstoneProject.Domain.Dtos.ResponseDto;
using CapstoneProject.Domain.Entities;
using CapstoneProject.Shared.RequestParameter.ModelParameters;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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
        /// <summary>
        /// Description: Enables a mentor to create an apppintment schedule for an assigned mentee
        /// </summary>
        /// <param name="appointmentScheduleRequest"></param>

        [HttpPost("create-appointment-schedule")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<AppointmentScheduleResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> CreateAppointmentScheduleAsync([FromBody] AppointmentScheduleRequestDto appointmentScheduleRequest)
        {
            var result = await _appointmentScheduleService.CreateAppointmentScheduleAsync(appointmentScheduleRequest);
            return Ok(result);
        }
        /// <summary>
        /// Description: This endpoint returns a list of all appointment schedules by all registered mentors
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all-appointment-sechedule")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<IEnumerable<AppointmentScheduleResponseDto>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> GetAllAppointmentSchedulesAsync([FromQuery] AppointmentScheduleRequestInputParameter parameter)
        {
            var result = await _appointmentScheduleService.GetAllSchedulesAsync(parameter);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.Data.MetaData));
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Description: Gets an appointment schedule using its Id and the mentors Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        [HttpGet("get-appointment-schedule-by-Id")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<AppointmentScheduleResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> GetAppointmentScheduleAsync(string id)
        {
            var result = await _appointmentScheduleService.GetAppointmentScheduleByIdAsync(id);
            return Ok(result);
        }
        /// <summary>
        /// Description: This endpoint deletes an appointment schedule using its Id and the mentors Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete-appointment-schedule")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<AppointmentScheduleResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> DeleteAppointmentScheduleAsync(string id)
        {
            var result = await _appointmentScheduleService.DeleteAppointmentScheduleAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Description: This endpoint enables a mentor to update the details of the appointment schedule using its Id and the mentors Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="appointmentScheduleRequest"></param>
        /// <returns></returns>
        [HttpPut("update-appointment-schedule")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<AppointmentScheduleResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> UpdateAppointmentScheduleAsync(string id, [FromBody] AppointmentScheduleRequestDto appointmentScheduleRequest)
        {
            var result = await _appointmentScheduleService.UpdateAppointmentScheduleAsync(id, appointmentScheduleRequest);
            return Ok(result);
        }
    }
   
}
