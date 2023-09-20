using CapstoneProject.Application.Services.Abstractions;
using CapstoneProject.Application.Services.Implementations;
using CapstoneProject.Domain.Dtos.RequestDto;
using CapstoneProject.Domain.Enums;
using CapstoneProject.Infrastructure.Repositories.Abstractions;
using CapstoneProject.Shared.RequestParameter.ModelParameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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

        // PUT api/<MentorController>
        [HttpPut]
        public async Task<IActionResult> UpdateMentorAsync(string id, [FromForm] MentorRequestDto mentorRequest)
        {
            var result = await _mentorService.UpdateMentorAsync(id, mentorRequest);
            return Ok(result);
        }
        //GET ALL api/<MentorController>
        [HttpGet]
        public async Task<IActionResult> GetAllMentorsAsync(int pageNumber)
        {
            var mentors = await _mentorService.GetAllMentorsAsync(pageNumber);
            /*Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(mentors.Data));
            return Ok(mentors.Data);*/
            return Ok(mentors);
        }
        //GET By Id api/<MentorController>
        [HttpGet("byId")]
        public async Task<IActionResult> GetMentorByIdAsync(string id)
        {
            var mentor = await _mentorService.GetMentorByIdAsync(id);
            return Ok(mentor);
        }
        //DELETE api/<MentorController>
        [HttpDelete]
        public async Task<IActionResult> DeleteMentorById(string id,[FromForm] MentorRequestDto mentorRequest)
        {
            var mentor = await _mentorService.DeleteMentorAsync(id);
            return Ok(mentor);
        }
        //POST api/<MentorController>
        [HttpPost("image")]
        public IActionResult UploadProfileImageAsync(string id, IFormFile file)
        {
            var picture = _mentorService.UploadProfileImageAsync(id, file);
            if (picture.Result.Succeeded)
            {
                return Ok(new { ImageUrl = picture.Result.Data.Item2 });
            }
            return NotFound();
        }
        //Get api/<MentorController>
        [HttpGet("byOrganization")]
        public async Task<IActionResult> GetMentorsByOrganizationAsync(string organization)
        {
            var mentor = await _mentorService.GetMentorByOrganizationAsync(organization);
            return Ok(mentor);
        }
        //Get api/<MentorController>
        [HttpGet("byCommunicationChannel")]
        public async Task<IActionResult> GetMentorByCommunicationChannel(string communicationChannel)
        {
            var mentor = await _mentorService.GetMentorByCommunicationChannelAsync(communicationChannel);
            return Ok(mentor);
        }
        //Get api/<MentorController>
        [HttpGet("byIsAvailable")]
        public async Task<IActionResult> GetMeentorByIsAvailableAsync()
        {
            var mentor = await _mentorService.GetMentorByIsAvailableAsync(true);
            return Ok(mentor);
        }
    }
}
