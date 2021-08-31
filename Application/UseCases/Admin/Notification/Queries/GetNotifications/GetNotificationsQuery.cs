using MediatR;
using System.Collections.Generic;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Notification.Queries.GetNotifications
{
    public class GetNotificationsQuery : AdminPaginationQuery, IRequest<GetNotificationsDto>
    {
    }
}
