using BurnoutinhosProject.Connection;
using BurnoutinhosProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BurnoutinhosProject.Repository
{
    public class NotificationRepository
    {
        private readonly AppDbContext _context;
        public NotificationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Notification>> GetAllAsync()
        {
            return await _context.Notification.ToListAsync();
        }
        public async Task<Notification?> GetByIdAsync(int id)
        {
            return await _context.Notification.FindAsync(id);
        }

        public async Task<IEnumerable<Notification>> GetByUserIdAsync(int userId)
        {
            return await _context.Notification.Where(n => n.UserId == userId).ToListAsync();
        }

        public async Task<Notification> AddAsync(Notification notification)
        {
            _context.Notification.Add(notification);
            await _context.SaveChangesAsync();
            return notification;
        }

        public async Task<Notification?> UpdateAsync(Notification notification)
        {
            var existingNotification = await _context.Notification.FindAsync(notification.Id);
            if (existingNotification == null)
            {
                return null;
            }
            _context.Entry(existingNotification).CurrentValues.SetValues(notification);
            await _context.SaveChangesAsync();
            return existingNotification;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var notification = await _context.Notification.FindAsync(id);
            if (notification == null)
            {
                return false;
            }
            _context.Notification.Remove(notification);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
