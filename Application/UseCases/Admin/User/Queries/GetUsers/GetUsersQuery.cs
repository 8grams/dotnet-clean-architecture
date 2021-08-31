using System.Collections.Generic;
using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.User.Queries.GetUsers
{
    public class GetUsersQuery : AdminPaginationQuery, IRequest<GetUsersDto>
    {
        public string Type { set; get; }
        public IList<FilterParams> Filters { set; get; }
    }
}
