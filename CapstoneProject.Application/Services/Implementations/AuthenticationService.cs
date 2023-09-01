using AutoMapper;
using CapstoneProject.Application.Services.Abstractions;
using CapstoneProject.Domain.Dtos.RequestDto;
using CapstoneProject.Domain.Dtos.ResponseDto;
using CapstoneProject.Domain.Entities;
using CapstoneProject.Domain.Enums;
using CapstoneProject.Infrastructure.RepositoryManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CapstoneProject.Application.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;
        private User _user;

        public AuthenticationService(ILogger<AuthenticationService> logger, IMapper mapper, IConfiguration config, UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _config = config;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<StandardResponse<IdentityResult>> RegisterUser(UserRequestDto userRequest)
        {
            try
            {
                var userEmail = await _userManager.FindByEmailAsync(userRequest.Email);
                if (userEmail != null)
                {
                    return new StandardResponse<IdentityResult>
                    {
                        Succeeded = false,
                        Message = $"User with this {userRequest.Email} already exists. Choose another email to proceed"
                    };
                }

                User user = new User();
                user.Email = userRequest.Email;
                user.UserName = userRequest.Email;
                var result = await _userManager.CreateAsync(user, userRequest.Password);

                if (result.Succeeded)
                {
                    if (userRequest.Roles == Roles.Mentor)
                    {

                        await _userManager.AddToRoleAsync(user, Roles.Mentor.ToString());
                        //create a mentor and save it
                        var createMentor = new Mentor
                        {
                            FirstName = userRequest.FirstName,
                            LastName = userRequest.LastName,
                        };
                        await _unitOfWork.MentorRepository.CreateAsync(createMentor);
                        _unitOfWork.SaveAsync();

                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, Roles.Mentee.ToString());
                        //create and a mentee and save
                        var createMentee = new Mentee
                        {
                            FirstName = userRequest.FirstName,
                            LastName = userRequest.LastName,
                        };
                        await _unitOfWork.MenteeRepository.CreateAsync(createMentee);
                        _unitOfWork.SaveAsync();
                    }

                }
                return new StandardResponse<IdentityResult>
                {
                    Succeeded = true,
                    Message = $"User registered successfully"
                };
            }
            catch (Exception ex)
            {
                return new StandardResponse<IdentityResult>
                {
                    Succeeded = false,
                    Message = $"An error occured while registering user"
                };

            }
        }

        public async Task<bool> ValidateUser(UserLoginDto userForAuth)
        {
            _user = await _userManager.FindByEmailAsync(userForAuth.Email);
            var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password));
            if (!result)
                _logger.LogWarning($"{nameof(ValidateUser)}: Authentication failed. Wrong username of password");
            return result;
        }
        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"));
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.Name, _user.UserName)
            };
            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _config.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken
            (
            issuer: jwtSettings["validIssuer"],
            audience: jwtSettings["validAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
            signingCredentials: signingCredentials
            );
            return tokenOptions;
        }
    }
}

