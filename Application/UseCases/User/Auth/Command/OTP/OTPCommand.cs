using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Auth.Command.OTP
{
    public class OTPCommand : BaseQueryCommand, IRequest<OTPDto>
    {
        public OTPCommandData Data { set; get; }
    }

    public class OTPCommandData
    {
        public int UserId { set; get; }
        public string Pin { set; get; }
    }
}
