using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tourist.APPLICATION.DTO.Notification;
using Tourist.APPLICATION.Interface;
using Tourist.DOMAIN.model;

namespace Tourist.APPLICATION.UseCase.Notification
{
    public class NotificationUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<NotificationDTO>> GetAllAsync()
        {
            var notifications = await _unitOfWork.Notification
                .GetAllAsync(n => !n.IsDeleted);

            return notifications.Select(n => new NotificationDTO
            {
                NotificationId = n.NotificationId,
                Message = n.Message,
                CreatedAt = n.CreatedAt,
                Type = n.Type,
                UserId = n.UserId,
                IsRead = n.IsRead
            });
        }

        public async Task<GetNotificationDTOs?> GetByIdAsync(int id)
        {
            var notification = await _unitOfWork.Notification
                .GetByIdAsync(id);

            if (notification == null || notification.IsDeleted)
                return null;

            return new GetNotificationDTOs
            {
                NotificationId = notification.NotificationId,
                Message = notification.Message,
                CreatedAt = notification.CreatedAt,
                Type = notification.Type,
                UserId = notification.UserId
            };
        }

        public async Task<int> CreateAsync(CreateNotificationDTOs dto)
        {
            var notification = new Notification
            {
                Message = dto.Message,
                IsRead = dto.IsRead,
                Type = dto.Type,
                UserId = dto.UserId,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _unitOfWork.Notification.AddAsync(notification);
            await _unitOfWork.SaveChangesAsync();

            return notification.NotificationId;
        }

        public async Task<bool> UpdateAsync(UpdateNotificationDTOs dto)
        {
            if (!dto.NotificationId.HasValue)
                return false;

            var notification = await _unitOfWork.Notification
                .GetByIdAsync(dto.NotificationId.Value);

            if (notification == null || notification.IsDeleted)
                return false;

            if (dto.Message != null)
                notification.Message = dto.Message;

            if (dto.IsRead.HasValue)
                notification.IsRead = dto.IsRead.Value;

            if (dto.Type != null)
                notification.Type = dto.Type;

            if (dto.UserId != null)
                notification.UserId = dto.UserId;

            _unitOfWork.Notification.Update(notification);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var notification = await _unitOfWork.Notification
                .GetByIdAsync(id);

            if (notification == null || notification.IsDeleted)
                return false;

            await _unitOfWork.Notification.SoftDeleteAsync(notification);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteMultipleAsync(IEnumerable<int> ids)
        {
            var notifications = await _unitOfWork.Notification
                .GetAllAsync(n => ids.Contains(n.NotificationId) && !n.IsDeleted);

            if (!notifications.Any())
                return false;

            await _unitOfWork.Notification.SoftDeleteRangeAsync(notifications);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
