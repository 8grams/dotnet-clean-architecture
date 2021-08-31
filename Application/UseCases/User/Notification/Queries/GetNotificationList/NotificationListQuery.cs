using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Notification.Queries.GetNotificationList
{
    public class NotificationListQuery : PaginationQuery, IRequest<NotificationListDto>
    {
        
    }
}
