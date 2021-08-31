using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.UseCases.Admin.Notification.Models;

namespace SFIDWebAPI.Application.UseCases.Admin.Notification.Queries.GetNotification
{
    public class GetNotificationQueryHandler : IRequestHandler<GetNotificationQuery, GetNotificationDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public GetNotificationQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetNotificationDto> Handle(GetNotificationQuery request, CancellationToken cancellationToken)
        {
            var notification = await _context.Notifications.FindAsync(request.Id);

            var response = new GetNotificationDto()
            {
                Success = true,
                Message = "Notification is succefully retrieved"
            };

            if (notification != null)
            {
                response.Data = _mapper.Map<NotificationData>(notification);
            }
            else
            {
                response.Success = false;
                response.Message = "Notification is not found";
            }

            return response;
        }
    }
}
