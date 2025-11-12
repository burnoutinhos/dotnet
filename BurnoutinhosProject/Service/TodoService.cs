using BurnoutinhosProject.Models;
using BurnoutinhosProject.Repository;

namespace BurnoutinhosProject.Service
{
    public class TodoService
    {
        private readonly TodoRepository _todoRepository;

        public TodoService(TodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        
        public async Task<IEnumerable<Todo>> GetAllTodosAsync()
        {
            return await _todoRepository.GetAllAsync();
        }
        public async Task<Todo?> GetTodoByIdAsync(int id)
        {
            return await _todoRepository.GetByIdAsync(id);
        }
        public async Task<IEnumerable<Todo>> GetTodosByUserIdAsync(int userId)
        {
            return await _todoRepository.GetByUserIdAsync(userId);
        }
        public async Task<Todo> CreateTodoAsync(Todo todo)
        {
            return await _todoRepository.AddAsync(todo);
        }
        public async Task<Todo?> UpdateTodoAsync(Todo todo)
        {
            return await _todoRepository.UpdateAsync(todo);
        }
        public async Task<bool> DeleteTodoAsync(int id)
        {
            return await _todoRepository.DeleteAsync(id);
        }
    }
}
