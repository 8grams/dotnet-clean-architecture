using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Auth.Command.Login
{
    public class LoginCommand : BaseQueryCommand, IRequest<LoginDto>
    {
        public LoginCommandData Data { set; get; }
    }

    public class LoginCommandData
    {
        public string SalesCode { set; get; }
        public string Password { set; get; }
        public string DeviceId { set; get; }
    }
}
