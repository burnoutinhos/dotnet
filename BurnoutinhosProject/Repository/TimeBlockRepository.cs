using BurnoutinhosProject.Connection;
using BurnoutinhosProject.DTO;
using BurnoutinhosProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BurnoutinhosProject.Repository
{
    public class TimeBlockRepository
    {
        private readonly AppDbContext _context;

        public TimeBlockRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TimeBlock>> GetAllAsync()
        {
            return await _context.TimeBlock.ToListAsync();
        }

        public async Task<PagedResponseDTO<TimeBlock>> GetPagedAsync(PaginationParametersDTO parameters)
        {
            var totalRecords = await _context.TimeBlock.CountAsync();
            
            var timeBlocks = await _context.TimeBlock
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            return new PagedResponseDTO<TimeBlock>(timeBlocks, totalRecords, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<PagedResponseDTO<TimeBlock>> GetPagedByUserIdAsync(int userId, PaginationParametersDTO parameters)
        {
            var totalRecords = await _context.TimeBlock.CountAsync(tb => tb.UserId == userId);
            
            var timeBlocks = await _context.TimeBlock
                .Where(tb => tb.UserId == userId)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            return new PagedResponseDTO<TimeBlock>(timeBlocks, totalRecords, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<TimeBlock?> GetByIdAsync(int id)
        {
            return await _context.TimeBlock.FindAsync(id);
        }

        public async Task<TimeBlock> AddAsync(TimeBlock timeBlock)
        {
            _context.TimeBlock.Add(timeBlock);
            await _context.SaveChangesAsync();
            return timeBlock;
        }

        public async Task<TimeBlock?> UpdateAsync(TimeBlock timeBlock)
        {
            var existingTimeBlock = await _context.TimeBlock.FindAsync(timeBlock.Id);
            if (existingTimeBlock == null)
            {
                return null;
            }
            _context.Entry(existingTimeBlock).CurrentValues.SetValues(timeBlock);
            await _context.SaveChangesAsync();
            return existingTimeBlock;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var timeBlock = await _context.TimeBlock.FindAsync(id);
            if (timeBlock == null)
            {
                return false;
            }
            _context.TimeBlock.Remove(timeBlock);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
