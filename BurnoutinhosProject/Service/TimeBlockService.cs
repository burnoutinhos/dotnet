using BurnoutinhosProject.Connection;
using BurnoutinhosProject.DTO;
using BurnoutinhosProject.Models;
using BurnoutinhosProject.Repository;

namespace BurnoutinhosProject.Service
{
    public class TimeBlockService
    {
        private readonly TimeBlockRepository timeBlockRepository;

        public TimeBlockService(TimeBlockRepository _timeBlockRepository)
        {
            timeBlockRepository = _timeBlockRepository;
        }

        public async Task<IEnumerable<TimeBlock>> GetAllAsync()
        {
            return await timeBlockRepository.GetAllAsync();
        }

        public async Task<PagedResponseDTO<TimeBlock>> GetPagedAsync(PaginationParametersDTO parameters)
        {
            return await timeBlockRepository.GetPagedAsync(parameters);
        }

        public async Task<PagedResponseDTO<TimeBlock>> GetPagedByUserIdAsync(int userId, PaginationParametersDTO parameters)
        {
            return await timeBlockRepository.GetPagedByUserIdAsync(userId, parameters);
        }

        public async Task<TimeBlock?> GetByIdAsync(int id)
        {
            return await timeBlockRepository.GetByIdAsync(id);
        }

        public async Task<TimeBlock> AddAsync(TimeBlock timeBlock)
        {
            return await timeBlockRepository.AddAsync(timeBlock);
        }

        public async Task<TimeBlock?> UpdateAsync(TimeBlock timeBlock)
        {
            return await timeBlockRepository.UpdateAsync(timeBlock);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await timeBlockRepository.DeleteAsync(id);
        }

    }
}
