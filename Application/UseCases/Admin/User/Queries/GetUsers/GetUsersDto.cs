using System.Collections.Generic;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.UseCases.Admin.User.Models;

namespace SFIDWebAPI.Application.UseCases.Admin.User.Queries.GetUsers
{
    public class GetUsersDto : PaginationDto
    {
        public IList<UserData> Data { set; get; }
    }   
}
