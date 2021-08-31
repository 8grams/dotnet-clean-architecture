using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Auth.Command.Login
{
    public class LoginCommand : BaseAdminQueryCommand, IRequest<LoginDto>
    {
        public LoginCommandData Data { set; get; }
    }

    public class LoginCommandData
    {
        public string Email { set; get; }
        public string Password  { set; get; }
    }
}
