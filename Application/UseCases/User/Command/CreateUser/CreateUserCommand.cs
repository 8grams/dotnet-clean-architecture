using MediatR;
using WebApi.Application.Models.Query;

namespace WebApi.Application.UseCases.User.Command.CreateUser
{
    public class CreateUserCommand : BaseQueryCommand, IRequest<CreateUserDto>
    {
        public CreateUserData Data { set; get; }
    }

    public class CreateUserData
    {
        public string Name { set; get; }
        public string UserName { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public int Age { set; get; }
        public string FileByte { set; get; }
    }
}
