using Microsoft.AspNetCore.Mvc;
using BurnoutinhosProject.Service;

namespace BurnoutinhosProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProcedureController : ControllerBase
    {
        private readonly ProcedureService _procedureService;

        public ProcedureController(ProcedureService procedureService)
        {
            _procedureService = procedureService;
        }

        /// <summary>
        /// Insere um novo usuário através de procedure Oracle.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/procedure/user
        ///     {
        ///        "idUser": 1,
        ///        "nameUser": "João Silva",
        ///        "emailUser": "joao@example.com",
        ///        "language": "PT-BR",
        ///        "profileImage": "avatar.jpg",
        ///        "password": "senha123"
        ///     }
        ///
        /// </remarks>
        /// <param name="request">Dados do usuário a ser inserido.</param>
        /// <returns>Mensagem de sucesso ou erro.</returns>
        /// <response code="200">Usuário inserido com sucesso.</response>
        /// <response code="400">Erro ao inserir usuário.</response>
        [HttpPost("user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InsertUser([FromBody] UserInsertRequest request)
        {
            var result = await _procedureService.InsertUserAsync(
                request.IdUser,
                request.NameUser,
                request.EmailUser,
                request.Language,
                request.ProfileImage,
                request.Password
            );

            return result ? Ok(new { message = "Usuário inserido com sucesso" }) : BadRequest(new { message = "Erro ao inserir usuário" });
        }  


        /// <summary>
        /// Insere uma nova notificação através de procedure Oracle.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/procedure/notification
        ///     {
        ///        "idNotif": 1,
        ///        "updatedAt": "2025-01-10T14:30:00",
        ///        "messageNotif": "Lembrete de tarefa",
        ///        "createdAt": "2025-01-10T14:00:00",
        ///        "idUser": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="request">Dados da notificação a ser inserida.</param>
        /// <returns>Mensagem de sucesso ou erro.</returns>
        /// <response code="200">Notificação inserida com sucesso.</response>
        /// <response code="400">Erro ao inserir notificação.</response>
        [HttpPost("notification")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InsertNotification([FromBody] NotificationInsertRequest request)
        {
            var result = await _procedureService.InsertNotificationAsync(
                request.IdNotif,
                request.UpdatedAt,
                request.MessageNotif,
                request.CreatedAt,
                request.IdUser
            );

            return result ? Ok(new { message = "Notificação inserida com sucesso" }) : BadRequest(new { message = "Erro ao inserir notificação" });
        }


        /// <summary>
        /// Insere uma nova tarefa (todo) através de procedure Oracle.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/procedure/todo
        ///     {
        ///        "idTodo": 1,
        ///        "nameTodo": "Estudar SQL",
        ///        "startTodo": "2025-01-10T08:00:00",
        ///        "endTodo": "2025-01-10T09:00:00",
        ///        "createdAt": "2025-01-10T07:55:00",
        ///        "updatedAt": "2025-01-10T09:05:00",
        ///        "description": "Revisar conceitos básicos",
        ///        "isCompleted": "N",
        ///        "idUser": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="request">Dados da tarefa a ser inserida.</param>
        /// <returns>Mensagem de sucesso ou erro.</returns>
        /// <response code="200">Todo inserido com sucesso.</response>
        /// <response code="400">Erro ao inserir todo.</response>
        [HttpPost("todo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InsertTodo([FromBody] TodoInsertRequest request)
        {
            var result = await _procedureService.InsertTodoAsync(
                request.IdTodo,
                request.NameTodo,
                request.StartTodo,
                request.EndTodo,
                request.CreatedAt,
                request.UpdatedAt,
                request.Description,
                request.IsCompleted,
                request.IdUser
            );

            return result ? Ok(new { message = "Todo inserido com sucesso" }) : BadRequest(new { message = "Erro ao inserir todo" });
        }

        /// <summary>
        /// Insere uma nova sugestão através de procedure Oracle.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/procedure/suggestion
        ///     {
        ///        "idSuggestion": 1,
        ///        "suggestionDesc": "Adicionar mais detalhes ao estudo",
        ///        "createdAt": "2025-01-10T09:10:00",
        ///        "idTodo": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="request">Dados da sugestão a ser inserida.</param>
        /// <returns>Mensagem de sucesso ou erro.</returns>
        /// <response code="200">Sugestão inserida com sucesso.</response>
        /// <response code="400">Erro ao inserir sugestão.</response>
        [HttpPost("suggestion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InsertSuggestion([FromBody] SuggestionInsertRequest request)
        {
            var result = await _procedureService.InsertSuggestionAsync(
                request.IdSuggestion,
                request.SuggestionDesc,
                request.CreatedAt,
                request.IdTodo
            );

            return result ? Ok(new { message = "Sugestão inserida com sucesso" }) : BadRequest(new { message = "Erro ao inserir sugestão" });
        }


        /// <summary>
        /// Insere um novo bloco de tempo através de procedure Oracle.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/procedure/timeblock
        ///     {
        ///        "idTimebk": 1,
        ///        "nameTimebk": "Foco SQL",
        ///        "timeCount": 25,
        ///        "maxTimebk": 50,
        ///        "startTimebk": 0,
        ///        "createdAt": "2025-01-10T08:10:00",
        ///        "typeTimebk": "FOCUS",
        ///        "idUser": 1,
        ///        "typeTimer": "POMODORO",
        ///        "idTodo": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="request">Dados do bloco de tempo a ser inserido.</param>
        /// <returns>Mensagem de sucesso ou erro.</returns>
        /// <response code="200">TimeBlock inserido com sucesso.</response>
        /// <response code="400">Erro ao inserir timeblock.</response>
        [HttpPost("timeblock")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InsertTimeBlock([FromBody] TimeBlockInsertRequest request)
        {
            var result = await _procedureService.InsertTimeBlockAsync(
                request.IdTimebk,
                request.NameTimebk,
                request.TimeCount,
                request.MaxTimebk,
                request.StartTimebk,
                request.CreatedAt,
                request.TypeTimebk,
                request.IdUser,
                request.TypeTimer,
                request.IdTodo
            );

            return result ? Ok(new { message = "TimeBlock inserido com sucesso" }) : BadRequest(new { message = "Erro ao inserir timeblock" });
        }
    }
}