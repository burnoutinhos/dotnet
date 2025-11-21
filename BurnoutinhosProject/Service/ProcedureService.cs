using Oracle.ManagedDataAccess.Client;
using Microsoft.Extensions.Configuration;
using BurnoutinhosProject.Models;

namespace BurnoutinhosProject.Service
{
    public class ProcedureService
    {
        private readonly string _connectionString;
        private readonly ILogger<ProcedureService> _logger;


        public ProcedureService(IConfiguration configuration, ILogger<ProcedureService> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;
        }

        public async Task<bool> InsertUserAsync(int idUser, string nameUser, string emailUser, string language, string profileImage, string password)
        {
            var connection = new OracleConnection(_connectionString);
            
            try
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "BEGIN pkg_dml_burnoutinhos.prc_insert_user(:p_id_user, :p_name_user, :p_email_user, :p_language, :p_profile_image, :p_password); END;";
                
                command.Parameters.Add(new OracleParameter("p_id_user", idUser));
                command.Parameters.Add(new OracleParameter("p_name_user", nameUser));
                command.Parameters.Add(new OracleParameter("p_email_user", emailUser));
                command.Parameters.Add(new OracleParameter("p_language", language));
                command.Parameters.Add(new OracleParameter("p_profile_image", profileImage));
                command.Parameters.Add(new OracleParameter("p_password", password));

                await command.ExecuteNonQueryAsync();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                await connection.CloseAsync();
                await connection.DisposeAsync();
            }
        }

        public async Task<bool> InsertNotificationAsync(int idNotif, DateTime updatedAt, string messageNotif, DateTime createdAt, int idUser)
        {
            var connection = new OracleConnection(_connectionString);
            
            try
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "BEGIN pkg_dml_burnoutinhos.prc_insert_notification(:p_id_notif, :p_updated_at, :p_message_notif, :p_created_at, :p_id_user); END;";
                
                command.Parameters.Add(new OracleParameter("p_id_notif", idNotif));
                command.Parameters.Add(new OracleParameter("p_updated_at", OracleDbType.TimeStamp) { Value = updatedAt });
                command.Parameters.Add(new OracleParameter("p_message_notif", messageNotif));
                command.Parameters.Add(new OracleParameter("p_created_at", OracleDbType.TimeStamp) { Value = createdAt });
                command.Parameters.Add(new OracleParameter("p_id_user", idUser));

                await command.ExecuteNonQueryAsync();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                await connection.CloseAsync();
                await connection.DisposeAsync();
            }
        }

        public async Task<bool> InsertTodoAsync(int idTodo, string nameTodo, DateTime startTodo, DateTime endTodo, DateTime createdAt, DateTime updatedAt, string description, char isCompleted, int idUser)
        {
            var connection = new OracleConnection(_connectionString);
            
            try
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "BEGIN pkg_dml_burnoutinhos.prc_insert_todo(:p_id_todo, :p_name_todo, :p_start_todo, :p_end_todo, :p_created_at, :p_updated_at, :p_description, :p_is_completed, :p_id_user); END;";
                
                command.Parameters.Add(new OracleParameter("p_id_todo", idTodo));
                command.Parameters.Add(new OracleParameter("p_name_todo", nameTodo));
                command.Parameters.Add(new OracleParameter("p_start_todo", OracleDbType.Date) { Value = startTodo });
                command.Parameters.Add(new OracleParameter("p_end_todo", OracleDbType.Date) { Value = endTodo });
                command.Parameters.Add(new OracleParameter("p_created_at", OracleDbType.TimeStamp) { Value = createdAt });
                command.Parameters.Add(new OracleParameter("p_updated_at", OracleDbType.TimeStamp) { Value = updatedAt });
                command.Parameters.Add(new OracleParameter("p_description", description));
                command.Parameters.Add(new OracleParameter("p_is_completed", isCompleted.ToString()));
                command.Parameters.Add(new OracleParameter("p_id_user", idUser));

                await command.ExecuteNonQueryAsync();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                await connection.CloseAsync();
                await connection.DisposeAsync();
            }
        }

        public async Task<bool> InsertSuggestionAsync(int idSuggestion, string suggestionDesc, DateTime createdAt, int idTodo)
        {
            var connection = new OracleConnection(_connectionString);
            
            try
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "BEGIN pkg_dml_burnoutinhos.prc_insert_suggestion(:p_id_suggestion, :p_suggestion_desc, :p_created_at, :p_id_todo); END;";
                
                command.Parameters.Add(new OracleParameter("p_id_suggestion", idSuggestion));
                command.Parameters.Add(new OracleParameter("p_suggestion_desc", suggestionDesc));
                command.Parameters.Add(new OracleParameter("p_created_at", OracleDbType.TimeStamp) { Value = createdAt });
                command.Parameters.Add(new OracleParameter("p_id_todo", idTodo));

                await command.ExecuteNonQueryAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao inserir sugestão. IdSuggestion: {IdSuggestion}, TodoId: {TodoId}", idSuggestion, idTodo);

                return false;
            }
            finally
            {
                await connection.CloseAsync();
                await connection.DisposeAsync();
            }
        }

        public async Task<bool> InsertTimeBlockAsync(int idTimebk, string nameTimebk, int timeCount, int maxTimebk, int startTimebk, DateTime createdAt, string typeTimebk, int idUser, string typeTimer, int idTodo)
        {
            var connection = new OracleConnection(_connectionString);
            
            try
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "BEGIN pkg_dml_burnoutinhos.prc_insert_timeblock(:p_id_timebk, :p_name_timebk, :p_time_count, :p_max_timebk, :p_start_timebk, :p_created_at, :p_type_timebk, :p_id_user, :p_type_timer, :p_id_todo); END;";
                
                command.Parameters.Add(new OracleParameter("p_id_timebk", idTimebk));
                command.Parameters.Add(new OracleParameter("p_name_timebk", nameTimebk));
                command.Parameters.Add(new OracleParameter("p_time_count", timeCount));
                command.Parameters.Add(new OracleParameter("p_max_timebk", maxTimebk));
                command.Parameters.Add(new OracleParameter("p_start_timebk", startTimebk));
                command.Parameters.Add(new OracleParameter("p_created_at", OracleDbType.TimeStamp) { Value = createdAt });
                command.Parameters.Add(new OracleParameter("p_type_timebk", typeTimebk));
                command.Parameters.Add(new OracleParameter("p_id_user", idUser));
                command.Parameters.Add(new OracleParameter("p_type_timer", typeTimer));
                command.Parameters.Add(new OracleParameter("p_id_todo", idTodo));

                await command.ExecuteNonQueryAsync();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                await connection.CloseAsync();
                await connection.DisposeAsync();
            }
        }
    }
}