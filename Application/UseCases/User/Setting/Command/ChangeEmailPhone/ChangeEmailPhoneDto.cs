using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Setting.Command.ChangeEmailPhone
{
    public class ChangeEmailPhoneDto : BaseDto
    {
        public ProfileDto Data { set; get; }
    }
}
