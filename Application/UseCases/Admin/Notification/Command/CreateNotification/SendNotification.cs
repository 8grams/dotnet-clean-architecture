using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Models.Notifications;

namespace SFIDWebAPI.Application.UseCases.Admin.Notification.Command.CreateNotification
{
    public class SendNotification : INotification
    {
        public FCMMessage Payload { set; get; }

        public class SendNotificationHandler : INotificationHandler<SendNotification>
        {
            private readonly IFCMService _fcmService;
            private readonly ISFDDBContext _context;

            public SendNotificationHandler(ISFDDBContext context, IFCMService fcmService)
            {
                _fcmService = fcmService;
                _context = context;
            }

            public async Task Handle(SendNotification request, CancellationToken cancellationToken)
            {
                //if (Utils.IsDevelopment()) return;
                await _fcmService.SendAsync(request.Payload);
            }
        }
    }
}
