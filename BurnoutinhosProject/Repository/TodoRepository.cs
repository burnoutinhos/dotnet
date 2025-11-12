using BurnoutinhosProject.Connection;
using BurnoutinhosProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BurnoutinhosProject.Repository
{
    public class TodoRepository
    {

        private readonly AppDbContext _context;

        public TodoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Todo>> GetAllAsync()
        {
            return await _context.Todo.ToListAsync();
        }

        public async Task<Todo?> GetByIdAsync(int id)
        {
            return await _context.Todo.FindAsync(id);
        }

        public async Task<IEnumerable<Todo>> GetByUserIdAsync(int userId)
        {
            return await _context.Todo.Where(t => t.UserId == userId).ToListAsync();
        }

        public async Task<Todo> AddAsync(Todo todo)
        {
            _context.Todo.Add(todo);
            await _context.SaveChangesAsync();
            return todo;
        }

        public async Task<Todo?> UpdateAsync(Todo todo)
        {
            var existingTodo = await _context.Todo.FindAsync(todo.Id);
            if (existingTodo == null)
            {
                return null;
            }
            _context.Entry(existingTodo).CurrentValues.SetValues(todo);
            await _context.SaveChangesAsync();
            return existingTodo;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var todo = await _context.Todo.FindAsync(id);
            if (todo == null)
            {
                return false;
            }
            _context.Todo.Remove(todo);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
