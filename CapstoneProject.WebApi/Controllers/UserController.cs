using CapstoneProject.Application.Services.Abstractions;
using CapstoneProject.Application.Services.Implementations;
using CapstoneProject.Domain.Dtos.RequestDto;
using CapstoneProject.Shared.RequestParameter.ModelParameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        //GET ALL api/<UserController>
        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var result = await _userService.GetAllUserAsync();
            return Ok(result);
        }
        //GET BY ID api/<UserController>
        [HttpGet("ById")]
        public async Task<IActionResult> GetUserByIdAsync(string id)
        {
            var result = await _userService.GetUserById(id);
            return Ok(result);
        }
        //GET BY EMAIL api/<UserController>
        [HttpPut]
        public async Task<IActionResult> GetUserByEmailAsync(string email)
        {
            var result = await _userService.GetUserByEmail(email);
            return Ok(result);
        }
        //DELETE api/<UserController>
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _userService.DeleteUser(id);
            return Ok(result);
        }
    }
}
