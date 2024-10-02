using Microsoft.AspNetCore.Mvc;
using Vyg.Assessment.BE.Dtos;
using Vyg.Assessment.BE.Services.Contracts;

namespace Vyg.Assessment.BE.Controllers
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

        [HttpPost]
        [Route("create_user")]
        public async Task<IActionResult> CreateUserAsync(UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.CreateUser(userDto);

            return Ok(new { result.isCreated, result.message });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(UserLoginDto userLoginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.LoginAsync(userLoginDto.Email, userLoginDto.Password);

            return Ok(new { result.tokenDetails, result.message });
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> LogoutAsync(string token)
        {
            await _userService.LogoutAsync(token);

            return Ok();
        }
    }
}
