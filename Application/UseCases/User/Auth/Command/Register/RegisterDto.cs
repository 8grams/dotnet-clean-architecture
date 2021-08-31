using System;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Auth.Command.Register
{
    public class RegisterDto : BaseDto
    {
        public ProfileDto Data { set; get; }
    }
}
