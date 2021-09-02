using System.Collections.Generic;
using MediatR;
using WebApi.Application.Models.Query;

namespace WebApi.Application.UseCases.User.Command.DeleteUser
{
    public class DeleteUserCommand : BaseQueryCommand, IRequest<DeleteUserDto>
    {
        public IList<int?> Ids { set; get; }
    }
}