using System.Collections.Generic;
using WebApi.Application.Models.Query;
using WebApi.Application.UseCases.User.Models;

namespace WebApi.Application.UseCases.User.Queries.GetUsers
{
    public class GetUsersDto : PaginationDto
    {
        public IList<UserData> Data { set; get; }
    }
}
