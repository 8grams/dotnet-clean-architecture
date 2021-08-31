using System.Collections.Generic;
using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.User.Command.DeleteUser
{
    public class DeleteUserCommand : BaseAdminQueryCommand, IRequest<DeleteUserDto>
    {
        public IList<int?> Ids { set; get; }
    }
}