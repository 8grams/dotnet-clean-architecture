using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Auth.Command.Login
{
    public class LoginDto : BaseDto
    {
        public ProfileDto Data { set; get; }
    }
}
