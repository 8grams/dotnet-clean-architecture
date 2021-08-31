using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;

namespace SFIDWebAPI.Application.UseCases.User.Notification.Command.UpdateNotification
{
    public class UpdateNotificationCommandHandler : IRequestHandler<UpdateNotificationCommand, UpdateNotificationDto>
    {
        private readonly ISFDDBContext _context;

        public UpdateNotificationCommandHandler(ISFDDBContext context)
        {
            _context = context;
        }

        public async Task<UpdateNotificationDto> Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
        {
            foreach (var notificationData in request.Data)
            {
                var status = await _context.NotificationStatuses
                    .Where(e => e.NotificationId == notificationData.Id)
                    .Where(e => e.UserId == request.UserId)
                    .FirstOrDefaultAsync();

                if (status == null)
                {
                    status = new Domain.Entities.NotificationStatus
                    {
                        NotificationId = notificationData.Id,
                        UserId = request.UserId,
                        IsDeleted = false,
                        HasRead = false
                    };

                    _context.NotificationStatuses.Add(status);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                var notifs = await _context.NotificationStatuses
                    .Where(e => e.UserId == request.UserId)
                    .Where(e => e.HasRead == false)
                    .ToListAsync();
                
                notifs.ForEach(a => a.HasRead = true);
                _context.NotificationStatuses.UpdateRange(notifs);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return new UpdateNotificationDto
            {
                Success = true,
                Message = "Notifications are successfully updated",
                Origin = "update_notification.success.default"
            };
        }
    }
}
