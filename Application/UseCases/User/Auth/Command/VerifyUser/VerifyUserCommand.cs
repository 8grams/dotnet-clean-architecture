using MediatR;

namespace SFIDWebAPI.Application.UseCases.User.Auth.Command.VerifyUser
{
    public class VerifyUserCommand : IRequest<VerifyUserDto>
    {
        public VerifyUserCommandData Data { set; get; }
    }

    public class VerifyUserCommandData
    {
        public string SalesCode { set; get; }
    }
}
