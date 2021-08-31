using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Setting.Command.ChangeEmailPhone
{
    public class ChangeEmailPhoneCommand : BaseQueryCommand, IRequest<ChangeEmailPhoneDto>
    {
        public ChangeEmailPhoneData Data { set; get; }
    }

    public class ChangeEmailPhoneData
    {
        public string Type { set; get; }
        public string Identifier { set; get; }
    }
}
