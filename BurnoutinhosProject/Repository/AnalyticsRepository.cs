using BurnoutinhosProject.Connection;
using BurnoutinhosProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BurnoutinhosProject.Repository
{
    public class AnalyticsRepository
    {
        private readonly AppDbContext _context;

        public AnalyticsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Analytics>> GetAllAsync()
        {
            return await _context.Analytics.ToListAsync();
        }

        public async Task<Analytics?> GetByIdAsync(int id)
        {
            return await _context.Analytics.FindAsync(id);
        }

        public async Task<Analytics> AddAsync(Analytics analytics)
        {
            _context.Analytics.Add(analytics);
            await _context.SaveChangesAsync();
            return analytics;
        }

        public async Task<Analytics?> UpdateAsync(Analytics analytics)
        {
            var existingAnalytics = await _context.Analytics.FindAsync(analytics.Id);
            if (existingAnalytics == null)
            {
                return null;
            }
            _context.Entry(existingAnalytics).CurrentValues.SetValues(analytics);
            await _context.SaveChangesAsync();
            return existingAnalytics;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var analytics = await _context.Analytics.FindAsync(id);
            if (analytics == null)
            {
                return false;
            }
            _context.Analytics.Remove(analytics);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
