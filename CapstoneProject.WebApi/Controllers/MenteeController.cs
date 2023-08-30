using CapstoneProject.Application.Services.Abstractions;
using CapstoneProject.Domain.Dtos.RequestDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        // POST api/<MenteeController>
        [HttpPost]
        public async Task<IActionResult> CreateMenteeAsync([FromBody] MenteeRequestDto menteeRequest)
        {
            var result = await _menteeService.CreateMenteeAsync(menteeRequest);
            return Ok(result);
        }
    }
}
