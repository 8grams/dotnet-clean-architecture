using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.UserProfile.Queries.CheckNotification
{
    public class CheckNotificationQuery : BaseQueryCommand, IRequest<CheckNotificationDto>
    {
        
    }
}
