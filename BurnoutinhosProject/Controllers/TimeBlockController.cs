using BurnoutinhosProject.DTO;
using BurnoutinhosProject.Models;
using BurnoutinhosProject.Service;
using Microsoft.AspNetCore.Mvc;

namespace BurnoutinhosProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimeBlockController : ControllerBase
    {
        private readonly TimeBlockService _timeBlockService;

        public TimeBlockController(TimeBlockService timeBlockService)
        {
            _timeBlockService = timeBlockService;
        }

        /// <summary>
        /// Retorna todos os blocos de tempo cadastrados.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /timeblock
        ///
        /// </remarks>
        /// <returns>Lista de blocos de tempo.</returns>
        /// <response code="200">Retorna a lista de blocos de tempo.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TimeBlock>>> GetTimeBlocks()
        {
            var timeBlocks = await _timeBlockService.GetAllAsync();
            return Ok(timeBlocks);
        }

        /// <summary>
        /// Retorna blocos de tempo cadastrados com paginação.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /timeblock/paged?pageNumber=1&amp;pageSize=10
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
        /// <returns>Lista paginada de blocos de tempo.</returns>
        /// <response code="200">Retorna a lista paginada de blocos de tempo.</response>
        [HttpGet("paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponseDTO<TimeBlock>>> GetPagedTimeBlocks(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var parameters = new PaginationParametersDTO
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var pagedTimeBlocks = await _timeBlockService.GetPagedAsync(parameters);
            return Ok(pagedTimeBlocks);
        }

        /// <summary>
        /// Retorna blocos de tempo de um usuário específico com paginação.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /timeblock/user/1/paged?pageNumber=1&amp;pageSize=10
        ///
        /// Exemplo de resposta:
        ///
        ///     {
        ///       "pageNumber": 1,
        ///       "pageSize": 10,
        ///       "totalPages": 2,
        ///       "totalRecords": 18,
        ///       "hasPrevious": false,
        ///       "hasNext": true,
        ///       "data": [...]
        ///     }
        ///
        /// </remarks>
        /// <param name="userId">ID do usuário.</param>
        /// <param name="pageNumber">Número da página (padrão: 1).</param>
        /// <param name="pageSize">Tamanho da página (padrão: 10, máximo: 50).</param>
        /// <returns>Lista paginada de blocos de tempo do usuário.</returns>
        /// <response code="200">Retorna a lista paginada de blocos de tempo do usuário.</response>
        [HttpGet("user/{userId}/paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponseDTO<TimeBlock>>> GetPagedTimeBlocksByUserId(
            int userId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var parameters = new PaginationParametersDTO
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var pagedTimeBlocks = await _timeBlockService.GetPagedByUserIdAsync(userId, parameters);
            return Ok(pagedTimeBlocks);
        }

        /// <summary>
        /// Retorna um bloco de tempo específico por ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /timeblock/1
        ///
        /// </remarks>
        /// <param name="id">ID do bloco de tempo.</param>
        /// <returns>Bloco de tempo encontrado.</returns>
        /// <response code="200">Retorna o bloco de tempo.</response>
        /// <response code="404">Bloco de tempo não encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TimeBlock>> GetTimeBlock(int id)
        {
            var timeBlock = await _timeBlockService.GetByIdAsync(id);
            if (timeBlock == null)
            {
                return NotFound();
            }
            return Ok(timeBlock);
        }

        /// <summary>
        /// Cria um novo bloco de tempo.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /timeblock
        ///     {
        ///        "name": "Foco SQL",
        ///        "timeCount": 25,
        ///        "maxTime": 50,
        ///        "startTime": 0,
        ///        "createdAt": "2025-01-10T08:10:00",
        ///        "type": "FOCUS",
        ///        "userId": 1,
        ///        "timerType": "POMODORO",
        ///        "todoId": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="timeBlock">Objeto do bloco de tempo a ser criado.</param>
        /// <returns>Bloco de tempo criado.</returns>
        /// <response code="201">Bloco de tempo criado com sucesso.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<TimeBlock>> CreateTimeBlock([FromBody] TimeBlock timeBlock)
        {
            var createdTimeBlock = await _timeBlockService.AddAsync(timeBlock);
            return CreatedAtAction(nameof(GetTimeBlock), new { id = createdTimeBlock.Id }, createdTimeBlock);
        }

        /// <summary>
        /// Atualiza um bloco de tempo existente.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /timeblock/1
        ///     {
        ///        "id": 1,
        ///        "name": "Foco SQL Atualizado",
        ///        "timeCount": 30,
        ///        "maxTime": 60,
        ///        "startTime": 0,
        ///        "createdAt": "2025-01-10T08:10:00",
        ///        "type": "FOCUS",
        ///        "userId": 1,
        ///        "timerType": "POMODORO",
        ///        "todoId": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="id">ID do bloco de tempo.</param>
        /// <param name="timeBlock">Objeto do bloco de tempo com dados atualizados.</param>
        /// <returns>Bloco de tempo atualizado.</returns>
        /// <response code="200">Bloco de tempo atualizado com sucesso.</response>
        /// <response code="400">IDs incongruentes.</response>
        /// <response code="404">Bloco de tempo não encontrado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TimeBlock>> UpdateTimeBlock(int id, [FromBody] TimeBlock timeBlock)
        {
            if (id != timeBlock.Id)
            {
                return BadRequest();
            }
            var updatedTimeBlock = await _timeBlockService.UpdateAsync(timeBlock);
            if (updatedTimeBlock == null)
            {
                return NotFound();
            }
            return Ok(updatedTimeBlock);
        }

        /// <summary>
        /// Deleta um bloco de tempo.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     DELETE /timeblock/1
        ///
        /// </remarks>
        /// <param name="id">ID do bloco de tempo a ser deletado.</param>
        /// <returns>Sem conteúdo.</returns>
        /// <response code="204">Bloco de tempo deletado com sucesso.</response>
        /// <response code="404">Bloco de tempo não encontrado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTimeBlock(int id)
        {
            var deleted = await _timeBlockService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
