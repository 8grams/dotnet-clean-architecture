using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Setting.Command.OTP.Trigger
{
    public class OTPTriggerCommand : BaseQueryCommand, IRequest<BaseDto>
    {
        
    }
}
