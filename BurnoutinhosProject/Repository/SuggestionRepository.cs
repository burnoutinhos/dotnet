using BurnoutinhosProject.Connection;
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
