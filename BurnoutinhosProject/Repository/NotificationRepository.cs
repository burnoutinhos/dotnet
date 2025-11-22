using BurnoutinhosProject.Connection;
using BurnoutinhosProject.DTO;
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
        public async Task<PagedResponseDTO<Notification>> GetPagedAsync(PaginationParametersDTO parameters)
        {
            var totalRecords = await _context.Notification.CountAsync();
            
            var notifications = await _context.Notification
                .OrderByDescending(n => n.CreatedAt)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            return new PagedResponseDTO<Notification>(notifications, totalRecords, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<PagedResponseDTO<Notification>> GetPagedByUserIdAsync(int userId, PaginationParametersDTO parameters)
        {
            var totalRecords = await _context.Notification.CountAsync(n => n.UserId == userId);
            
            var notifications = await _context.Notification
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            return new PagedResponseDTO<Notification>(notifications, totalRecords, parameters.PageNumber, parameters.PageSize);
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
