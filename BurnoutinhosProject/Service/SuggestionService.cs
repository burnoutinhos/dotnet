using BurnoutinhosProject.Models;
using BurnoutinhosProject.Repository;

namespace BurnoutinhosProject.Service
{
    public class SuggestionService
    {

        private readonly SuggestionRepository _suggestionRepository;
        public SuggestionService(SuggestionRepository suggestionRepository)
        {
            _suggestionRepository = suggestionRepository;
        }
        public async Task<IEnumerable<Suggestion>> GetAllAsync()
        {
            return await _suggestionRepository.GetAllAsync();
        }
        public async Task<Suggestion?> GetByIdAsync(int id)
        {
            return await _suggestionRepository.GetByIdAsync(id);
        }
        public async Task<Suggestion> AddAsync(Suggestion suggestion)
        {
            return await _suggestionRepository.AddAsync(suggestion);
        }
        public async Task<Suggestion?> UpdateAsync(Suggestion suggestion)
        {
            return await _suggestionRepository.UpdateAsync(suggestion);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _suggestionRepository.DeleteAsync(id);
        }
    }
}
