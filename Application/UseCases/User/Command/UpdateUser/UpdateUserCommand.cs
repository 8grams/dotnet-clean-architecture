using MediatR;
using WebApi.Application.Models.Query;

namespace WebApi.Application.UseCases.User.Command.UpdateUser
{
    public class UpdateUserCommand : BaseQueryCommand, IRequest<UpdateUserDto>
    {
        public UpdateUserData Data { set; get; }
    }

    public class UpdateUserData
    {
        public int Id { set;get; }
        public string Name { set; get; }
        public string UserName { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public int Age { set; get; }
        public string FileByte { set; get; }
    }
}
