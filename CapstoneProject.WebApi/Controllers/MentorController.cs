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

        // PUT api/<MentorController>
        [HttpPut]
        public async Task<IActionResult> UpdateMentorAsync(string id, [FromBody] MentorRequestDto mentorRequest)
        {
            var result = await _mentorService.UpdateMentorAsync(id, mentorRequest);
            return Ok(result);
        }
        //GET ALL api/<MentorController>
        [HttpGet]
        public async Task<IActionResult> GetAllMentorsAsync()
        {
            var result = await _mentorService.GetAllMentorsAsync();
            return Ok(result);
        }
        //GET By Id api/<MentorController>
        [HttpGet("ById")]
        public async Task<IActionResult> GetMentorByIdAsync(string id)
        {
            var result = await _mentorService.GetMentorByIdAsync(id);
            return Ok(result);
        }
        //DELETE api/<MentorController>
        [HttpDelete]
        public async Task<IActionResult> DeleteMentorById(string id, MentorRequestDto mentorRequest)
        {
            var result = await _mentorService.DeleteMentorAsync(id);
            return Ok(result);
        }
    }
}
