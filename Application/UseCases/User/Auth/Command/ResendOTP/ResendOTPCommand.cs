using MediatR;

namespace SFIDWebAPI.Application.UseCases.User.Auth.Command.ResendOTP
{
    public class ResendOTPCommand : IRequest<ResendOTPDto>
    {
        public ResendOTPData Data { set; get; }
    }

    public class ResendOTPData
    {
        public int UserId { set; get; }
        public string Concern { set; get; }
    }
}
