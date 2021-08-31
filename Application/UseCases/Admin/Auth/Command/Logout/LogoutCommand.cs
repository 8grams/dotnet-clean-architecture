using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Auth.Command.Logout
{
    public class LogoutCommand : BaseQueryCommand, IRequest<LogoutDto>
    {
        public string AuthToken { set; get; }
    }
}
