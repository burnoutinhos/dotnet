using System.Text.RegularExpressions;
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

        /// <summary>
        /// Realiza o login do usuário e retorna um token JWT.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /auth/login
        ///     {
        ///        "email": "usuario@exemplo.com",
        ///        "password": "senha123"
        ///     }
        ///
        /// </remarks>
        /// <param name="loginUser">Objeto contendo email e senha do usuário.</param>
        /// <returns>Token JWT para autenticação.</returns>
        /// <response code="200">Retorna o token de autenticação.</response>
        /// <response code="401">Email ou senha inválidos.</response>

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginUser)
        {
            var user = await _userService.GetAllUsersAsync();
            var existingUser = user.FirstOrDefault(u => u.Email == loginUser.Email && u.Password == loginUser.Password);

            if (string.IsNullOrWhiteSpace(loginUser.Email) || 
                !Regex.IsMatch(loginUser.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                return BadRequest($"O email '{loginUser.Email}' é inválido.");
            }

            if (existingUser == null)
            {
                return Unauthorized("Invalid email or password.");
            }
            var token = _tokenService.GenerateToken(existingUser.Email);
            return Ok(new { Token = token });
        }
    }
}
