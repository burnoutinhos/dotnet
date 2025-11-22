using BurnoutinhosProject.DTO;
using BurnoutinhosProject.Models;
using BurnoutinhosProject.Service;
using Microsoft.AspNetCore.Mvc;

namespace BurnoutinhosProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SuggestionController : ControllerBase
    {
        private readonly SuggestionService suggestionService;

        public SuggestionController(SuggestionService suggestionService)
        {
            this.suggestionService = suggestionService;
        }

        /// <summary>
        /// Retorna todas as sugestões cadastradas.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /suggestion
        ///
        /// </remarks>
        /// <returns>Lista de sugestões.</returns>
        /// <response code="200">Retorna a lista de sugestões.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Suggestion>>> GetSuggestions()
        {
            var suggestions = await suggestionService.GetAllAsync();
            return Ok(suggestions);
        }

        /// <summary>
        /// Retorna sugestões cadastradas com paginação.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /suggestion/paged?pageNumber=1&amp;pageSize=10
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
        /// <param name="pageNumber">Número da página (padrão: 1).</param>
        /// <param name="pageSize">Tamanho da página (padrão: 10, máximo: 50).</param>
        /// <returns>Lista paginada de sugestões.</returns>
        /// <response code="200">Retorna a lista paginada de sugestões.</response>
        [HttpGet("paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponseDTO<Suggestion>>> GetPagedSuggestions(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var parameters = new PaginationParametersDTO
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var pagedSuggestions = await suggestionService.GetPagedAsync(parameters);
            return Ok(pagedSuggestions);
        }

        /// <summary>
        /// Retorna sugestões de uma tarefa específica com paginação.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /suggestion/todo/1/paged?pageNumber=1&amp;pageSize=10
        ///
        /// Exemplo de resposta:
        ///
        ///     {
        ///       "pageNumber": 1,
        ///       "pageSize": 10,
        ///       "totalPages": 1,
        ///       "totalRecords": 8,
        ///       "hasPrevious": false,
        ///       "hasNext": false,
        ///       "data": [...]
        ///     }
        ///
        /// </remarks>
        /// <param name="todoId">ID da tarefa.</param>
        /// <param name="pageNumber">Número da página (padrão: 1).</param>
        /// <param name="pageSize">Tamanho da página (padrão: 10, máximo: 50).</param>
        /// <returns>Lista paginada de sugestões da tarefa.</returns>
        /// <response code="200">Retorna a lista paginada de sugestões da tarefa.</response>
        [HttpGet("todo/{todoId}/paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponseDTO<Suggestion>>> GetPagedSuggestionsByTodoId(
            int todoId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var parameters = new PaginationParametersDTO
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var pagedSuggestions = await suggestionService.GetPagedByTodoIdAsync(todoId, parameters);
            return Ok(pagedSuggestions);
        }

        /// <summary>
        /// Retorna uma sugestão específica por ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /suggestion/1
        ///
        /// </remarks>
        /// <param name="id">ID da sugestão.</param>
        /// <returns>Sugestão encontrada.</returns>
        /// <response code="200">Retorna a sugestão.</response>
        /// <response code="404">Sugestão não encontrada.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Suggestion>> GetSuggestionById(int id)
        {
            var suggestion = await suggestionService.GetByIdAsync(id);
            if (suggestion == null)
            {
                return NotFound();
            }
            return Ok(suggestion);
        }

        /// <summary>
        /// Cria uma nova sugestão.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /suggestion
        ///     {
        ///        "suggestionDescription": "Adicionar mais detalhes ao estudo",
        ///        "userId": 1,
        ///        "createdAt": "2025-01-10T09:10:00"
        ///     }
        ///
        /// </remarks>
        /// <param name="suggestion">Objeto da sugestão a ser criada.</param>
        /// <returns>Sugestão criada.</returns>
        /// <response code="201">Sugestão criada com sucesso.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Suggestion>> CreateSuggestion([FromBody] Suggestion suggestion)
        {
            var createdSuggestion = await suggestionService.AddAsync(suggestion);
            return CreatedAtAction(nameof(GetSuggestionById), new { id = createdSuggestion.Id }, createdSuggestion);
        }

        /// <summary>
        /// Atualiza uma sugestão existente.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /suggestion/1
        ///     {
        ///        "id": 1,
        ///        "suggestionDescription": "Sugestão atualizada",
        ///        "userId": 1,
        ///        "createdAt": "2025-01-10T09:10:00"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">ID da sugestão.</param>
        /// <param name="suggestion">Objeto da sugestão com dados atualizados.</param>
        /// <returns>Sugestão atualizada.</returns>
        /// <response code="200">Sugestão atualizada com sucesso.</response>
        /// <response code="400">IDs incongruentes.</response>
        /// <response code="404">Sugestão não encontrada.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Suggestion>> UpdateSuggestion(int id, [FromBody] Suggestion suggestion)
        {
            if (id != suggestion.Id)
            {
                return BadRequest("Ids incongruentes.");
            }
            var updatedSuggestion = await suggestionService.UpdateAsync(suggestion);
            if (updatedSuggestion == null)
            {
                return NotFound();
            }
            return Ok(updatedSuggestion);
        }

        /// <summary>
        /// Deleta uma sugestão.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     DELETE /suggestion/1
        ///
        /// </remarks>
        /// <param name="id">ID da sugestão a ser deletada.</param>
        /// <returns>Sem conteúdo.</returns>
        /// <response code="204">Sugestão deletada com sucesso.</response>
        /// <response code="404">Sugestão não encontrada.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSuggestion(int id)
        {
            var deleted = await suggestionService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
