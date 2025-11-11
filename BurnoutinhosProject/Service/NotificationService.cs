using BurnoutinhosProject.Models;
using BurnoutinhosProject.Repository;

namespace BurnoutinhosProject.Service
{
    public class NotificationService
    {
        private readonly NotificationRepository _notificationRepository;

        public NotificationService(NotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<IEnumerable<Notification>> GetAllNotificationsAsync()
        {
            return await _notificationRepository.GetAllAsync();
        }

        public async Task<Notification?> GetNotificationByIdAsync(int id)
        {
            return await _notificationRepository.GetByIdAsync(id);
        }

        public async Task<Notification> CreateNotificationAsync(Notification notification)
        {
            return await _notificationRepository.AddAsync(notification);
        }
        public async Task<Notification?> UpdateNotificationAsync(Notification notification)
        {
            return await _notificationRepository.UpdateAsync(notification);
        }
        public async Task<bool> DeleteNotificationAsync(int id)
        {
            return await _notificationRepository.DeleteAsync(id);
        }
    }
}
