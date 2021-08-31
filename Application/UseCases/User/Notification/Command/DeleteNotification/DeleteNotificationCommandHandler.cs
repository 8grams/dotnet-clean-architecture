using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;

namespace SFIDWebAPI.Application.UseCases.User.Notification.Command.DeleteNotification
{
    public class DeleteNotificationCommandHandler : IRequestHandler<DeleteNotificationCommand, DeleteNotificationDto>
    {
        private readonly ISFDDBContext _context;

        public DeleteNotificationCommandHandler(ISFDDBContext context)
        {
            _context = context;
        }

        public async Task<DeleteNotificationDto> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        {
            foreach (var id in request.Ids)
            {
                var status = await _context.NotificationStatuses
                    .Where(e => e.NotificationId == id)
                    .Where(e => e.UserId == request.UserId)
                    .FirstOrDefaultAsync();

                if (status == null)
                {
                    status = new Domain.Entities.NotificationStatus
                    {
                        NotificationId = id.GetValueOrDefault(),
                        UserId = request.UserId,
                        IsDeleted = false,
                        HasRead = false
                    };

                    _context.NotificationStatuses.Add(status);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                status.IsDeleted = true;
                _context.NotificationStatuses.Update(status);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return new DeleteNotificationDto
            {
                Success = true,
                Message = "Notifications are successfully deleted",
                Origin = "delete_notification.success.default"
            };
        }
    }
}
