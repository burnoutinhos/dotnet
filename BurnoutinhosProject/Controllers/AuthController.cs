using BurnoutinhosProject.DTO;
using BurnoutinhosProject.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BurnoutinhosProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly TokenService _tokenService;
        public AuthController(UserService userService, TokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginUser)
        {
            var user = await _userService.GetAllUsersAsync();
            var existingUser = user.FirstOrDefault(u => u.Email == loginUser.Email && u.Password == loginUser.Password);
            if (existingUser == null)
            {
                return Unauthorized("Invalid email or password.");
            }
            var token = _tokenService.GenerateToken(existingUser.Email);
            return Ok(new { Token = token });
        }
    }
}
