using BurnoutinhosProject.Connection;
using BurnoutinhosProject.DTO;
using BurnoutinhosProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BurnoutinhosProject.Repository
{
    public class SuggestionRepository
    {
        private readonly AppDbContext _context;

        public SuggestionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Suggestion>> GetAllAsync()
        {
            return await _context.Suggestion.ToListAsync();
        }

        public async Task<Suggestion?> GetByIdAsync(int id)
        {
            return await _context.Suggestion.FindAsync(id);
        }

        public async Task<PagedResponseDTO<Suggestion>> GetPagedAsync(PaginationParametersDTO parameters)
        {
            var totalRecords = await _context.Suggestion.CountAsync();
            
            var suggestions = await _context.Suggestion
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            return new PagedResponseDTO<Suggestion>(suggestions, totalRecords, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<PagedResponseDTO<Suggestion>> GetPagedByTodoIdAsync(int todoId, PaginationParametersDTO parameters)
        {
            var totalRecords = await _context.Suggestion.CountAsync(s => s.TodoId == todoId);
            
            var suggestions = await _context.Suggestion
                .Where(s => s.TodoId == todoId)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            return new PagedResponseDTO<Suggestion>(suggestions, totalRecords, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<Suggestion> AddAsync(Suggestion suggestion)
        {
            _context.Suggestion.Add(suggestion);
            await _context.SaveChangesAsync();
            return suggestion;
        }

        public async Task<Suggestion?> UpdateAsync(Suggestion suggestion)
        {
            var existingSuggestion = await _context.Suggestion.FindAsync(suggestion.Id);
            if (existingSuggestion == null)
            {
                return null;
            }
            _context.Entry(existingSuggestion).CurrentValues.SetValues(suggestion);
            await _context.SaveChangesAsync();
            return existingSuggestion;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var suggestion = await _context.Suggestion.FindAsync(id);
            if (suggestion == null)
            {
                return false;
            }
            _context.Suggestion.Remove(suggestion);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
