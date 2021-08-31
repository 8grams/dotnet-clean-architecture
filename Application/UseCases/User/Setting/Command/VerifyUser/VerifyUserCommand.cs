using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Setting.Command.VerifyUser
{
    public class VerifyUserCommand : BaseQueryCommand, IRequest<VerifyUserDto>
    {
        public VerifyUserCommandData Data { set; get; }
    }

    public class VerifyUserCommandData
    {
        public string Password { set; get; }
    }
}
