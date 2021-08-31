using System.Collections.Generic;
using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Bulletin.Command.DeleteBulletin
{
    public class DeleteBulletinCommand : BaseAdminQueryCommand, IRequest<DeleteBulletinDto>
    {
        public IList<int?> Ids { set; get; }
    }
}