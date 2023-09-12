using CapstoneProject.Application.Services.Abstractions;
using CapstoneProject.Application.Services.Implementations;
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
        
        //GET ALL api/<MenteeController>
        [HttpGet]
        public async Task<IActionResult> GetAllMenteesAsync()
        {
            var result = await _menteeService.GetAllMenteesAsync();
            return Ok(result);
        }
        //GET BY ID api/<MenteeController>
        [HttpGet("ById")]
        public async Task<IActionResult> GetMenteeByIdAsync(string id)
        {
            var result = await _menteeService.GetMenteeByIdAsync(id);
            return Ok(result);
        }
        //PUT api/<MenteeController>
        [HttpPut]
        public async Task<IActionResult> UpdateMenteeAsync(string id,[FromBody] MenteeRequestDto menteeRequest)
        {
            
            var result = await _menteeService.UpdateMenteeAsync(id, menteeRequest);
            return Ok(result);
        }
        //POST api/<MentorController>
        [HttpPost("Image/{id}")]
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