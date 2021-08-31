using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.User.Queries.GetUser
{
    public class GetUserQuery : BaseAdminQueryCommand, IRequest<GetUserDto>
    {
        public int Id { set; get; }
    }
}
