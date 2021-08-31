using System.Collections.Generic;
using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.FAQ.Command.DeleteFAQ
{
    public class DeleteFAQCommand : BaseAdminQueryCommand, IRequest<DeleteFAQDto>
    {
        public IList<int?> Ids { set; get; }
    }
}