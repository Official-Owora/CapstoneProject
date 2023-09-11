using AutoMapper;
using CapstoneProject.Application.Services.Abstractions;
using CapstoneProject.Domain.Dtos.RequestDto;
using CapstoneProject.Domain.Dtos.ResponseDto;
using CapstoneProject.Domain.Entities;
using CapstoneProject.Domain.Enums;
using CapstoneProject.Infrastructure.RepositoryManager;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
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
        public User _user;


        public AuthenticationService(ILogger<AuthenticationService> logger, IMapper mapper, IConfiguration config, UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _config = config;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<StandardResponse<IdentityResult>> RegisterUser(UserRequestDto userRequest, MenteeRequestDto menteeRequest, MenteeRequestDto menteeRequestDto)
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
                    if (userRequest.Roles == UserType.Mentor)
                    {

                        await _userManager.AddToRoleAsync(user, UserType.Mentor.ToString());
                        var getUser = await _userManager.FindByEmailAsync(userRequest.Email);
                        //create a mentor and save it
                        var createMentor = new Mentor
                        {
                            UserId = getUser.Id,
                            FirstName = userRequest.FirstName,
                            LastName = userRequest.LastName,
                        };
                        await _unitOfWork.MentorRepository.CreateAsync(createMentor);
                        await _unitOfWork.SaveAsync();

                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, UserType.Mentee.ToString());
                        var getUser = await _userManager.FindByEmailAsync(userRequest.Email);

                        //create and a mentee and save
                        var createMentee = new Mentee
                        {
                            UserId = getUser.Id,
                            FirstName = userRequest.FirstName,
                            LastName = userRequest.LastName,
                        };
                        await _unitOfWork.MenteeRepository.CreateAsync(createMentee);
                        //Assigning Mentee a Mentor
                        var mentors = await _unitOfWork.MentorRepository.GetAllMentorsAsync();
                        Mentor mentor = null;
                        
                        foreach (var mentorDB in mentors)
                        {
                            if (mentorDB.TechTrack == userRequest.TechTrack && mentorDB.ProgrammingLanguage == userRequest.MainProgrammingLanguage && mentor == null)
                            {
                                mentor = mentorDB;
                            }
                            var mentee = _mapper.Map<User>(userRequest);
                            /*if(mentor != null)
                            {
                                mentee.Me
                            }*/
                        }
                        
                        
                        _unitOfWork.UserRepository.Update(user);

                        //Saving the Mentee to the database
                        await _unitOfWork.SaveAsync();
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
            var user = await _userManager.FindByEmailAsync(userForAuth.Email);
            var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password));
            if (!result)
                _logger.LogWarning($"{nameof(ValidateUser)}: Authentication failed. Wrong username or password");
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

