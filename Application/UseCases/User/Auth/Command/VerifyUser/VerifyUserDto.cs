using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Auth.Command.VerifyUser
{
    public class VerifyUserDto : BaseDto
    {
        public ProfileDto Data { set; get; }
    }
}
