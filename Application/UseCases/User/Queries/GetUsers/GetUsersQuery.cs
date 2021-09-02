using MediatR;
using WebApi.Application.Models.Query;

namespace WebApi.Application.UseCases.User.Queries.GetUsers
{
    public class GetUsersQuery : PaginationQuery, IRequest<GetUsersDto>
    {
        
    }
}
