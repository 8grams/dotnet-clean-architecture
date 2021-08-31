using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Setting.Command.ResetPassword
{
    public class ResetPasswordCommand : BaseQueryCommand, IRequest<ResetPasswordDto>
    {
        public ResetPasswordCommandData Data { set; get; }
    }

    public class ResetPasswordCommandData
    {
        public string OldPassword { set; get; }
        public string NewPassword { set; get; }
    }
}
