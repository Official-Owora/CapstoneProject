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
    public class MenteeController : ControllerBase
    {
        private readonly IMenteeService _menteeService;

        public MenteeController(IMenteeService menteeService)
        {
            _menteeService = menteeService;
        }
        /// <summary>
        /// Description: This endpoint returns a list of registered users that are mentees only.
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all-mentees")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<IEnumerable<MenteeResponseDto>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> GetAllMenteesAsync([FromQuery] MenteeRequestInputParameter parameter)
        {
            var result = await _menteeService.GetAllMenteesAsync(parameter);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.Data.MetaData));
            return StatusCode(result.StatusCode, result);
        }
        /// <summary>
        /// Description: This endpoint returns a mentee's detail using its Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get-mentee-by-id")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<MenteeResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> GetMenteeByIdAsync(string id)
        {
            var result = await _menteeService.GetMenteeByIdAsync(id);
            return Ok(result);
        }
        /// <summary>
        /// Description: This enpoint allows mentees to update their details after registeration and login. It requires the mentee's Id (userId) and the paired mentor's Id (mentorId) and details of their tech track, mentorship duration and years of experience. It returns the enums as integers rather than strings.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="menteeRequest"></param>
        /// <returns></returns>
        [HttpPut("update-mentee-details")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<MenteeResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> UpdateMenteeAsync(string id,[FromForm] MenteeRequestDto menteeRequest)
        {
            
            var result = await _menteeService.UpdateMenteeAsync(id, menteeRequest);
            return Ok(result);
        }
        /// <summary>
        /// Description: This endpoint allows a mentee to delete their account.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete-mentee-by-id")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<MenteeResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> DeleteMenteeAsync(string id)
        {
            var result = await _menteeService.DeleteMenteeAsync(id);
            return Ok(result);
        }
        /// <summary>
        /// Description: This endpoint enables a mentee to upload the profile picture. It returns the response as a string, which is a link to the profile image.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("upload-mentee-image")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]
        public IActionResult UploadProfileImageAsync(string id, IFormFile file)
        {
            var result = _menteeService.UploadProfileImageAsync(id, file);
            if (result.Result.Succeeded)
            {
                return Ok(new { ImageUrl = result.Result.Data.Item2 });
            }
            return NotFound();
        }
    }
}