using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.UseCases.Admin.User.Models;

namespace SFIDWebAPI.Application.UseCases.Admin.User.Queries.GetUser
{
    public class GetUserDto : PaginationDto
    {
        public UserData Data { set; get; }
    }   
}
