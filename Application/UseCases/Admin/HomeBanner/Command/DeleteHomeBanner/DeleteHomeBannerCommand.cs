using System.Collections.Generic;
using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.HomeBanner.Command.DeleteHomeBanner
{
    public class DeleteHomeBannerCommand : BaseAdminQueryCommand, IRequest<DeleteHomeBannerDto>
    {
        public IList<int?> Ids { set; get; }
    }
}