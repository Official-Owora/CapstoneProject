using CapstoneProject.Application.Services.Abstractions;
using CapstoneProject.Domain.Dtos.RequestDto;
using CapstoneProject.Infrastructure.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
