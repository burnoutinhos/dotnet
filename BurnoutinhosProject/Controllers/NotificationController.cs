using BurnoutinhosProject.DTO;
using BurnoutinhosProject.Models;
using BurnoutinhosProject.Service;
using Microsoft.AspNetCore.Mvc;

namespace BurnoutinhosProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationService _notificationService;

        public NotificationController(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }


        /// <summary>
        /// Retorna todas as notificações cadastradas.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /notification
        ///
        /// </remarks>
        /// <returns>Lista de notificações.</returns>
        /// <response code="200">Retorna a lista de notificações.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Notification>>> GetAllNotifications()
        {
            var notifications = await _notificationService.GetAllNotificationsAsync();
            return Ok(notifications);
        }

        /// <summary>
        /// Retorna notificações cadastradas com paginação.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /notification/paged?pageNumber=1&amp;pageSize=10
        ///
        /// Exemplo de resposta:
        ///
        ///     {
        ///       "pageNumber": 1,
        ///       "pageSize": 10,
        ///       "totalPages": 5,
        ///       "totalRecords": 45,
        ///       "hasPrevious": false,
        ///       "hasNext": true,
        ///       "data": [...]
        ///     }
        ///
        /// </remarks>
        /// <param name="pageNumber">Número da página (padrão: 1).</param>
        /// <param name="pageSize">Tamanho da página (padrão: 10, máximo: 50).</param>
        /// <returns>Lista paginada de notificações.</returns>
        /// <response code="200">Retorna a lista paginada de notificações.</response>
        [HttpGet("paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponseDTO<Notification>>> GetPagedNotifications(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var parameters = new PaginationParametersDTO
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var pagedNotifications = await _notificationService.GetPagedNotificationsAsync(parameters);
            return Ok(pagedNotifications);
        }

        /// <summary>
        /// Retorna notificações de um usuário específico com paginação.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /notification/user/1/paged?pageNumber=1&amp;pageSize=10
        ///
        /// Exemplo de resposta:
        ///
        ///     {
        ///       "pageNumber": 1,
        ///       "pageSize": 10,
        ///       "totalPages": 3,
        ///       "totalRecords": 22,
        ///       "hasPrevious": false,
        ///       "hasNext": true,
        ///       "data": [...]
        ///     }
        ///
        /// </remarks>
        /// <param name="userId">ID do usuário.</param>
        /// <param name="pageNumber">Número da página (padrão: 1).</param>
        /// <param name="pageSize">Tamanho da página (padrão: 10, máximo: 50).</param>
        /// <returns>Lista paginada de notificações do usuário.</returns>
        /// <response code="200">Retorna a lista paginada de notificações do usuário.</response>
        [HttpGet("user/{userId}/paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponseDTO<Notification>>> GetPagedNotificationsByUserId(
            int userId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var parameters = new PaginationParametersDTO
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var pagedNotifications = await _notificationService.GetPagedNotificationsByUserIdAsync(userId, parameters);
            return Ok(pagedNotifications);
        }

        /// <summary>
        /// Retorna todas as notificações de um usuário específico.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /notification/user/1
        ///
        /// </remarks>
        /// <param name="userId">ID do usuário.</param>
        /// <returns>Lista de notificações do usuário.</returns>
        /// <response code="200">Retorna a lista de notificações do usuário.</response>
        [HttpGet("user/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotificationsByUserId(int userId)
        {
            var notifications = await _notificationService.GetNotificationsByUserIdAsync(userId);
            return Ok(notifications);
        }


        /// <summary>
        /// Retorna uma notificação específica por ID.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /notification/1
        ///
        /// </remarks>
        /// <param name="id">ID da notificação.</param>
        /// <returns>Notificação encontrada.</returns>
        /// <response code="200">Retorna a notificação.</response>
        /// <response code="404">Notificação não encontrada.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Notification>> GetNotificationById(int id)
        {
            var notification = await _notificationService.GetNotificationByIdAsync(id);
            if (notification == null)
            {
                return NotFound();
            }
            return Ok(notification);
        }


        /// <summary>
        /// Cria uma nova notificação.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /notification
        ///     {
        ///        "message": "Lembrete de tarefa",
        ///        "userId": 1,
        ///        "isRead": false,
        ///        "createdAt": "2025-01-10T14:00:00"
        ///     }
        ///
        /// </remarks>
        /// <param name="notification">Objeto da notificação a ser criada.</param>
        /// <returns>Notificação criada.</returns>
        /// <response code="201">Notificação criada com sucesso.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Notification>> CreateNotification([FromBody] Notification notification)
        {
            var createdNotification = await _notificationService.CreateNotificationAsync(notification);
            return CreatedAtAction(nameof(GetNotificationById), new { id = createdNotification.Id }, createdNotification);
        }


        /// <summary>
        /// Atualiza uma notificação existente.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /notification/1
        ///     {
        ///        "id": 1,
        ///        "message": "Lembrete atualizado",
        ///        "userId": 1,
        ///        "isRead": true,
        ///        "createdAt": "2025-01-10T14:00:00"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">ID da notificação.</param>
        /// <param name="notification">Objeto da notificação com dados atualizados.</param>
        /// <returns>Notificação atualizada.</returns>
        /// <response code="200">Notificação atualizada com sucesso.</response>
        /// <response code="400">IDs incongruentes.</response>
        /// <response code="404">Notificação não encontrada.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Notification>> UpdateNotification(int id, [FromBody] Notification notification)
        {
            if (id != notification.Id)
            {
                return BadRequest("Ids incongruents.");
            }
            var updatedNotification = await _notificationService.UpdateNotificationAsync(notification);
            if (updatedNotification == null)
            {
                return NotFound();
            }
            return Ok(updatedNotification);
        }


        /// <summary>
        /// Deleta uma notificação.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     DELETE /notification/1
        ///
        /// </remarks>
        /// <param name="id">ID da notificação a ser deletada.</param>
        /// <returns>Sem conteúdo.</returns>
        /// <response code="204">Notificação deletada com sucesso.</response>
        /// <response code="404">Notificação não encontrada.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var result = await _notificationService.DeleteNotificationAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
