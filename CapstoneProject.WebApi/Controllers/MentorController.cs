using CapstoneProject.Application.Services.Abstractions;
using CapstoneProject.Domain.Dtos.RequestDto;
using CapstoneProject.Infrastructure.Repositories.Abstractions;
using CapstoneProject.Shared.RequestParameter.ModelParameters;
using Microsoft.AspNetCore.Http;
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
        // POST api/<MentorController>
        [HttpPost]
        public async Task<IActionResult> CreateMentorAsync([FromBody] MentorRequestDto mentorRequest)
        {
            var result = await _mentorService.CreateMentorAsync(mentorRequest);
            return Ok(result);
        }
        // PUT api/<MentorController>
        [HttpPut]
        public async Task<IActionResult> UpdateMentorAsync(string id, [FromBody] MentorRequestDto mentorRequest)
        {
            var result = await _mentorService.UpdateMentorAsync(id, mentorRequest);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMentorsAsync()
        {
            var result = await _mentorService.GetAllMentorsAsync();
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteMentorById(string id, MentorRequestDto mentorRequest)
        {
            var result = await _mentorService.DeleteMentorAsync(id);
            return Ok(result);
        }
    }
}
