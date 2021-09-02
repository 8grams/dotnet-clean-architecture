using WebApi.Application.Models.Query;
using WebApi.Application.UseCases.User.Models;

namespace WebApi.Application.UseCases.User.Queries.GetUser
{
    public class GetUserDto : BaseDto
    {
        public UserData Data { set; get; }
    }
}
