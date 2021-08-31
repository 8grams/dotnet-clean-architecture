using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFIDWebAPI.Application.Interfaces;

namespace SFIDWebAPI.Application.UseCases.Admin.Notification.Command.DeleteNotification
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
                var notification = await _context.Notifications.FindAsync(id);
                if (notification != null) _context.Notifications.Remove(notification);
            }

            await _context.SaveChangesAsync(cancellationToken);
            
            return new DeleteNotificationDto
            {
                Success = true,
                Message = "Notification has been successfully deleted"
            };
        }
    }
}