using BurnoutinhosProject.DTO;
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
        public async Task<PagedResponseDTO<Suggestion>> GetPagedAsync(PaginationParametersDTO parameters)
        {
            return await _suggestionRepository.GetPagedAsync(parameters);
        }

        public async Task<PagedResponseDTO<Suggestion>> GetPagedByTodoIdAsync(int todoId, PaginationParametersDTO parameters)
        {
            return await _suggestionRepository.GetPagedByTodoIdAsync(todoId, parameters);
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
