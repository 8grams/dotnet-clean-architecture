using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Auth.Command.Register
{
    public class RegisterCommand : BaseQueryCommand, IRequest<RegisterDto>
    {
        public RegisterCommandData Data { set; get; }
    }

    public class RegisterCommandData
    {
        public string Email { set; get; }
        public string Phone { set; get; }
        public string Password { set; get; }
        public string SalesCode { set; get; }
        public string DeviceId { set; get; }
    }
}
