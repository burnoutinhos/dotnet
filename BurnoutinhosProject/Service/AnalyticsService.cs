using BurnoutinhosProject.Models;
using BurnoutinhosProject.Repository;

namespace BurnoutinhosProject.Service
{
    public class AnalyticsService
    {

        private readonly AnalyticsRepository _analyticsRepository;

        public AnalyticsService(AnalyticsRepository analyticsRepository)
        {
            _analyticsRepository = analyticsRepository;
        }

        public async Task<IEnumerable<Analytics>> GetAllAsync()
        {
            return await _analyticsRepository.GetAllAsync();
        }

        public async Task<Analytics?> GetByIdAsync(int id)
        {
            return await _analyticsRepository.GetByIdAsync(id);
        }

        public async Task<Analytics> AddAsync(Analytics analytics)
        {
            return await _analyticsRepository.AddAsync(analytics);
        }

        public async Task<Analytics?> UpdateAsync(Analytics analytics)
        {
            return await _analyticsRepository.UpdateAsync(analytics);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _analyticsRepository.DeleteAsync(id);
        }

    }
}
