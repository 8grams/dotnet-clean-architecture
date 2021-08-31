using System.Collections.Generic;
using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.AdminProfile.Command.DeleteAdmin
{
    public class DeleteAdminCommand : BaseAdminQueryCommand, IRequest<DeleteAdminDto>
    {
        public IList<int?> Ids { set; get; }
    }
}