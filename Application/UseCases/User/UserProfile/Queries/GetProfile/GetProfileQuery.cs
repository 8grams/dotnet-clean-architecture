using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.UserProfile.Queries.GetProfile
{
    public class GetProfileQuery : BaseQueryCommand, IRequest<GetProfileDto>
    {
        public int Id { set; get; }
    }
}
