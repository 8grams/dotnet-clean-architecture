using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Auth.Command.ForgotPassword
{
    public class ForgotPasswordCommand : BaseQueryCommand, IRequest<ForgotPasswordDto>
    {
        public ForgotPasswordCommandData Data { set; get; }
    }

    public class ForgotPasswordCommandData
    {
        public string SalesCode { set; get; }
        public string Type { set; get; }
        public string Identifier { set; get; }
    }
}
