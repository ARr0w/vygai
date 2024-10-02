using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Vyg.Assessment.BE.Dtos;
using Vyg.Assessment.BE.Repositories;
using Vyg.Assessment.BE.Services.Contracts;
using Vyg.Assessment.BE.Utility;

namespace Vyg.Assessment.BE.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(UserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<(bool isCreated, string message)> CreateUser(UserDto userDto)
        {
            try
            {
                var existingUser = await _userRepository.GetUserAsync(userDto.Email);

                if (existingUser != null)
                {
                    return (false, "User already exists.");
                }

                userDto.Password = userDto.Password.ConvertToHash();

                return (await _userRepository.CreateUserAsync(userDto), string.Empty);
            }
            catch (Exception ex)
            {
                return (false, "Something went wrong.");
            }

        }

        public async Task<(object? tokenDetails, string message)> LoginAsync(string email, string password)
        {
            try
            {
                var user = await _userRepository.GetUserAsync(email, password.ConvertToHash());

                if (user == null)
                {
                    return (string.Empty, "Invalid user name or password.");
                }

                var tokenDetails = GenerateJwtToken(user.Email);

                return (new { tokenDetails.token, tokenDetails.expiry, user.FirstName, user.LastName }, string.Empty);
            }
            catch (Exception ex)
            {
                return (null, "Something went wrong.");
            }
        }

        public async Task LogoutAsync(string token)
        {
            JwtTokenHandler.AddToken(token);

            await Task.CompletedTask;
        }

        private (string token, DateTime expiry) GenerateJwtToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);
            var expiry = DateTime.UtcNow.AddHours(1);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = expiry,
                Issuer = _configuration["Jwt:Issuer"]!,
                Audience = _configuration["Jwt:Audience"]!,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return (tokenHandler.WriteToken(token), expiry);
        }
    }
}
