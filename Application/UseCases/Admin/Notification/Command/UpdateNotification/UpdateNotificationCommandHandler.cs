using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Exceptions;

namespace SFIDWebAPI.Application.UseCases.Admin.Notification.Command.UpdateNotification
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
            var notification = await _context.Notifications.FindAsync(request.Data.Id);
            if (notification == null) throw new NotFoundException(nameof(Domain.Entities.Notification), request.Data.Id);

            notification.Title = request.Data.Title;
            notification.Content = request.Data.Content;
            notification.IsDeletable = request.Data.IsDeletable;
            notification.LastUpdateBy = request.AdminName;

            if (!string.IsNullOrEmpty(request.Data.Attachment))
            {

            }
            
            _context.Notifications.Update(notification);
            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateNotificationDto
            {
                Success = true,
                Message = "Notification has been successfully updated"
            };
        }
    }
}