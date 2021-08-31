using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.HomeBanner.Queries.GetHomeBanner
{
    public class GetHomeBannerQuery : BaseAdminQueryCommand, IRequest<GetHomeBannerDto>
    {
        public int Id { set; get; }
    }
}
