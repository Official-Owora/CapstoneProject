using CapstoneProject.Application.Services.Abstractions;
using CapstoneProject.Domain.Dtos.RequestDto;
using CapstoneProject.Domain.Dtos.ResponseDto;
using CapstoneProject.Shared.RequestParameter.ModelParameters;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json;

namespace CapstoneProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MentorController : ControllerBase
    {
        private readonly IMentorService _mentorService;

        public MentorController(IMentorService mentorService)
        {
            _mentorService = mentorService;
        }
        /// <summary>
        /// Description: This enpoint allows a mentor to update their details after registeration and login. It takes in the id of the mentor.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mentorRequest"></param>
        /// <returns></returns>
        [HttpPut("update-mentor")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<MentorResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> UpdateMentorAsync(string id, [FromForm] MentorRequestDto mentorRequest)
        {
            var result = await _mentorService.UpdateMentorAsync(id, mentorRequest);
            return Ok(result);
        }
        /// <summary>
        /// Description: This endpoint returns a list of registered users that are mentors only.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [HttpGet("get-all-mentors")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<IEnumerable<MentorResponseDto>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> GetAllMentorsAsync([FromQuery] MentorRequestInputParemeter parameter)
        {
            var mentors = await _mentorService.GetAllMentorsAsync(parameter);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(mentors.Data.MetaData));
            return StatusCode(mentors.StatusCode, mentors);
 
        }
        /// <summary>
        /// Description: This endpoint returns a mentor's detail using its Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get-mentor-by-Id")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<MentorResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> GetMentorByIdAsync(string id)
        {
            var mentor = await _mentorService.GetMentorByIdAsync(id);
            return Ok(mentor);
        }
        /// <summary>
        /// Description: This endpoint allows a mentor to delete their account.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mentorRequest"></param>
        /// <returns></returns>
        [HttpDelete("delete-mentor-by-Id")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<MentorResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> DeleteMentorById(string id,[FromForm] MentorRequestDto mentorRequest)
        {
            var mentor = await _mentorService.DeleteMentorAsync(id);
            return Ok(mentor);
        }
        /// <summary>
        /// Description: This endpoint enables a mentor to upload their profile picture. It returns the response as a string, which is a link to the profile image.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("upload-mentor-photo")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]
        public IActionResult UploadProfileImageAsync(string id, IFormFile file)
        {
            var picture = _mentorService.UploadProfileImageAsync(id, file);
            if (picture.Result.Succeeded)
            {
                return Ok(new { ImageUrl = picture.Result.Data.Item2 });
            }
            return NotFound();
        }
        /// <summary>
        /// Description: This endpoint returns a list of all mentors that work in a particular organization
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        [HttpGet("get-mentors-by-Organization")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<IEnumerable<MentorResponseDto>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> GetMentorsByOrganizationAsync(string organization)
        {
            var mentor = await _mentorService.GetMentorByOrganizationAsync(organization);
            return Ok(mentor);
        }
        /// <summary>
        /// Description: This endpoint returns a list of all mentors with a particular communication channel
        /// </summary>
        /// <param name="communicationChannel"></param>
        /// <returns></returns>
        [HttpGet("get-mentors-by-CommunicationChannel")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<IEnumerable<MentorResponseDto>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> GetMentorByCommunicationChannel(string communicationChannel)
        {
            var mentor = await _mentorService.GetMentorByCommunicationChannelAsync(communicationChannel);
            return Ok(mentor);
        }
        /// <summary>
        /// Description: This endpoint returns a list of all available mentors only. This is indicated by the field isAvailable. 
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all-available-mentors")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<IEnumerable<MentorResponseDto>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> GetMeentorByIsAvailableAsync()
        {
            var mentor = await _mentorService.GetMentorByIsAvailableAsync(true);
            return Ok(mentor);
        }
    }
}
