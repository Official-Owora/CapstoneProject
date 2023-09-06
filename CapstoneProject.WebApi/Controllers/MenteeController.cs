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
        [HttpGet]
        public async Task<IActionResult> GetAllMenteesAsync()
        {
            var result = await _menteeService.GetAllMenteesAsync();
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateMenteeAsync(string id,[FromBody] MenteeRequestDto menteeRequest)
        {
            var result = await _menteeService.UpdateMenteeAsync(id, menteeRequest);
            return Ok(result);
        }
       /* [HttpGet]
        public async Task<IActionResult> GetAllMenteesAsync()
        {
            var result = await _menteeService.GetAllMenteesAsync();
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.Data.Item1));
            return Ok(result.Data.Item2);
        }*/
    }
}
