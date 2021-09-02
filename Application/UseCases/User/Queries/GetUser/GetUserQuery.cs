using MediatR;
using WebApi.Application.Models.Query;

namespace WebApi.Application.UseCases.User.Queries.GetUser
{
    public class GetUserQuery : BaseQueryCommand, IRequest<GetUserDto>
    {
        public int Id { set; get; }
    }
}
