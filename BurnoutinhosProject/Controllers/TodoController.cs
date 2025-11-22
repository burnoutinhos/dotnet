using BurnoutinhosProject.DTO;
using BurnoutinhosProject.Models;
using BurnoutinhosProject.Service;
using Microsoft.AspNetCore.Mvc;

namespace BurnoutinhosProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {

        private readonly TodoService _todoService;

        public TodoController(TodoService todoService)
        {
            _todoService = todoService;
        }

        /// <summary>
        /// Retorna todas as tarefas cadastradas.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /todo
        ///
        /// </remarks>
        /// <returns>Lista de tarefas.</returns>
        /// <response code="200">Retorna a lista de tarefas.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Todo>>> GetAllTodos()
        {
            var todos = await _todoService.GetAllTodosAsync();
            return Ok(todos);
        }

        /// <summary>
        /// Retorna tarefas cadastradas com paginação.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /todo/paged?pageNumber=1&amp;pageSize=10
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
        /// <returns>Lista paginada de tarefas.</returns>
        /// <response code="200">Retorna a lista paginada de tarefas.</response>
        [HttpGet("paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponseDTO<Todo>>> GetPagedTodos(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var parameters = new PaginationParametersDTO
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var pagedTodos = await _todoService.GetPagedTodosAsync(parameters);
            return Ok(pagedTodos);
        }

        /// <summary>
        /// Retorna uma tarefa específica por ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /todo/1
        ///
        /// </remarks>
        /// <param name="id">ID da tarefa.</param>
        /// <returns>Tarefa encontrada.</returns>
        /// <response code="200">Retorna a tarefa.</response>
        /// <response code="404">Tarefa não encontrada.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Todo>> GetTodoById(int id)
        {
            var todo = await _todoService.GetTodoByIdAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }

        /// <summary>
        /// Retorna todas as tarefas de um usuário específico.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /todo/user/1
        ///
        /// </remarks>
        /// <param name="userId">ID do usuário.</param>
        /// <returns>Lista de tarefas do usuário.</returns>
        /// <response code="200">Retorna a lista de tarefas do usuário.</response>
        [HttpGet("user/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodosByUserId(int userId)
        {
            var todos = await _todoService.GetTodosByUserIdAsync(userId);
            return Ok(todos);
        }

        /// <summary>
        /// Retorna tarefas de um usuário específico com paginação.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /todo/user/1/paged?pageNumber=1&amp;pageSize=10
        ///
        /// Exemplo de resposta:
        ///
        ///     {
        ///       "pageNumber": 1,
        ///       "pageSize": 10,
        ///       "totalPages": 2,
        ///       "totalRecords": 15,
        ///       "hasPrevious": false,
        ///       "hasNext": true,
        ///       "data": [...]
        ///     }
        ///
        /// </remarks>
        /// <param name="userId">ID do usuário.</param>
        /// <param name="pageNumber">Número da página (padrão: 1).</param>
        /// <param name="pageSize">Tamanho da página (padrão: 10, máximo: 50).</param>
        /// <returns>Lista paginada de tarefas do usuário.</returns>
        /// <response code="200">Retorna a lista paginada de tarefas do usuário.</response>
        [HttpGet("user/{userId}/paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponseDTO<Todo>>> GetPagedTodosByUserId(
            int userId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var parameters = new PaginationParametersDTO
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var pagedTodos = await _todoService.GetPagedTodosByUserIdAsync(userId, parameters);
            return Ok(pagedTodos);
        }

        /// <summary>
        /// Cria uma nova tarefa.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /todo
        ///     {
        ///        "title": "Estudar SQL",
        ///        "description": "Revisar conceitos básicos",
        ///        "startDate": "2025-01-10T08:00:00",
        ///        "endDate": "2025-01-10T09:00:00",
        ///        "isCompleted": false,
        ///        "userId": 1,
        ///        "createdAt": "2025-01-10T07:55:00"
        ///     }
        ///
        /// </remarks>
        /// <param name="todo">Objeto da tarefa a ser criada.</param>
        /// <returns>Tarefa criada.</returns>
        /// <response code="201">Tarefa criada com sucesso.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Todo>> CreateTodo([FromBody] Todo todo)
        {
            var createdTodo = await _todoService.CreateTodoAsync(todo);
            return CreatedAtAction(nameof(GetTodoById), new { id = createdTodo.Id }, createdTodo);
        }

        /// <summary>
        /// Atualiza uma tarefa existente.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /todo/1
        ///     {
        ///        "id": 1,
        ///        "title": "Estudar SQL Avançado",
        ///        "description": "Revisar conceitos avançados",
        ///        "startDate": "2025-01-10T08:00:00",
        ///        "endDate": "2025-01-10T10:00:00",
        ///        "isCompleted": true,
        ///        "userId": 1,
        ///        "createdAt": "2025-01-10T07:55:00"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">ID da tarefa.</param>
        /// <param name="todo">Objeto da tarefa com dados atualizados.</param>
        /// <returns>Tarefa atualizada.</returns>
        /// <response code="200">Tarefa atualizada com sucesso.</response>
        /// <response code="400">IDs incongruentes.</response>
        /// <response code="404">Tarefa não encontrada.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Todo>> UpdateTodo(int id, [FromBody] Todo todo)
        {
            if (id != todo.Id)
            {
                return BadRequest();
            }
            var updatedTodo = await _todoService.UpdateTodoAsync(todo);
            if (updatedTodo == null)
            {
                return NotFound();
            }
            return Ok(updatedTodo);
        }

        /// <summary>
        /// Deleta uma tarefa.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     DELETE /todo/1
        ///
        /// </remarks>
        /// <param name="id">ID da tarefa a ser deletada.</param>
        /// <returns>Sem conteúdo.</returns>
        /// <response code="204">Tarefa deletada com sucesso.</response>
        /// <response code="404">Tarefa não encontrada.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var result = await _todoService.DeleteTodoAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
