using CapstoneProject.Application.Services.Abstractions;
using CapstoneProject.Domain.Dtos.RequestDto;
using CapstoneProject.Domain.Dtos.ResponseDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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
        /// <summary>
        /// Description: This endpoint registers users, who are either a mentor or a mentee. The point of registration also creates users. It also assignes a mentee to a mentor on registration if a mentor is available, and their Tech track, Mentorship duration, are the same, and the Mentor's year of experience is greater than the mentees year of experience. It takes in First Name, Last Name, Email Address and Password
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>

        [HttpPost("register-users")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<UserResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> RegisterUser([FromForm] UserRequestDto requestDto)
        {
            var result = await _authenticationService.RegisterUser(requestDto);
            return Ok(result);          
        }
        /// <summary>
        /// Description: This enpoint allows users to login using Email and Password. Only registered users are authorized to login.
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
               
        [HttpPost("login-users")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StandardResponse<UserResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]
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