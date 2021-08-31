using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Notification.Queries.GetNotification
{
    public class GetNotificationQuery : BaseAdminQueryCommand, IRequest<GetNotificationDto>
    {
        public int Id { set; get; }
    }
}
