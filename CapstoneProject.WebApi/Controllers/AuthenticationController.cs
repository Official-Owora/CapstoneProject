using CapstoneProject.Application.Services.Abstractions;
using CapstoneProject.Domain.Dtos.RequestDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CapstoneProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        // POST api/<AuthenticationController>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromForm] UserRequestDto requestDto)
        {
            var result = await _authenticationService.RegisterUser(requestDto);
            if (!result.Succeeded)
            {
                foreach (var error in result.Data.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(new { Message = "User registration failed. Mentor is unavailable" });
            }
            return Ok(result);          
        }
        //[Authorize(Roles ="Mentor")]        
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginDto userLogin)
        {
            if (!await _authenticationService.ValidateUser(userLogin))
            {
                return Unauthorized();
            }
            return Ok(new { token = await _authenticationService.CreateToken() });
        }
        
       
    }
}