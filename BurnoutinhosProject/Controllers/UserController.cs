using System.Text.RegularExpressions;
using BurnoutinhosProject.DTO;
using BurnoutinhosProject.Models;
using BurnoutinhosProject.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BurnoutinhosProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Retorna todos os usuários cadastrados.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /user
        ///
        /// </remarks>
        /// <returns>Lista de usuários.</returns>
        /// <response code="200">Retorna a lista de usuários.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// Retorna usuários cadastrados com paginação.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /user/paged?pageNumber=1&amp;pageSize=10
        ///
        /// Exemplo de resposta:
        ///
        ///     {
        ///       "pageNumber": 1,
        ///       "pageSize": 10,
        ///       "totalPages": 3,
        ///       "totalRecords": 25,
        ///       "hasPrevious": false,
        ///       "hasNext": true,
        ///       "data": [...]
        ///     }
        ///
        /// </remarks>
        /// <param name="pageNumber">Número da página (padrão: 1).</param>
        /// <param name="pageSize">Tamanho da página (padrão: 10, máximo: 50).</param>
        /// <returns>Lista paginada de usuários.</returns>
        /// <response code="200">Retorna a lista paginada de usuários.</response>
        [HttpGet("paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponseDTO<User>>> GetPagedUsers(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var parameters = new PaginationParametersDTO
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var pagedUsers = await _userService.GetPagedUsersAsync(parameters);
            return Ok(pagedUsers);
        }

        /// <summary>
        /// Retorna um usuário específico por ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /user/1
        ///
        /// </remarks>
        /// <param name="id">ID do usuário.</param>
        /// <returns>Usuário encontrado.</returns>
        /// <response code="200">Retorna o usuário.</response>
        /// <response code="404">Usuário não encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /user
        ///     {
        ///        "name": "João Silva",
        ///        "email": "joao@example.com",
        ///        "password": "senha123",
        ///        "preferredLanguage": "PT_BR",
        ///        "profileImage": "avatar.jpg"
        ///     }
        ///
        /// </remarks>
        /// <param name="user">Objeto do usuário a ser criado.</param>
        /// <returns>Usuário criado.</returns>
        /// <response code="201">Usuário criado com sucesso.</response>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<User>> CreateUser([FromBody] User user)
        {

            if (string.IsNullOrWhiteSpace(user.Email) || 
                !Regex.IsMatch(user.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                return BadRequest($"O email '{user.Email}' é inválido.");
            }

            var createdUser = await _userService.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }

        /// <summary>
        /// Atualiza um usuário existente.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /user/1
        ///     {
        ///        "id": 1,
        ///        "name": "João Silva Atualizado",
        ///        "email": "joao.novo@example.com",
        ///        "password": "novaSenha123",
        ///        "preferredLanguage": "EN_US",
        ///        "profileImage": "new_avatar.jpg"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">ID do usuário.</param>
        /// <param name="user">Objeto do usuário com dados atualizados.</param>
        /// <returns>Usuário atualizado.</returns>
        /// <response code="200">Usuário atualizado com sucesso.</response>
        /// <response code="400">IDs incongruentes.</response>
        /// <response code="404">Usuário não encontrado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> UpdateUser(int id, [FromBody] User user)
        {
            if (id != user.Id)
            {
                return BadRequest("User ID mismatch.");
            }

            if (string.IsNullOrWhiteSpace(user.Email) || 
                !Regex.IsMatch(user.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                return BadRequest($"O email '{user.Email}' é inválido.");
            }
            var updatedUser = await _userService.UpdateUserAsync(user);
            if (updatedUser == null)
            {
                return NotFound();
            }
            return Ok(updatedUser);
        }

        /// <summary>
        /// Deleta um usuário.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     DELETE /user/1
        ///
        /// </remarks>
        /// <param name="id">ID do usuário a ser deletado.</param>
        /// <returns>Sem conteúdo.</returns>
        /// <response code="204">Usuário deletado com sucesso.</response>
        /// <response code="404">Usuário não encontrado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await _userService.DeleteUserAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
