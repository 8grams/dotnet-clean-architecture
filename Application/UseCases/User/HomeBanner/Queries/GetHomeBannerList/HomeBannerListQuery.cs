using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.HomeBanner.Queries.GetHomeBannerList
{
    public class HomeBannerListQuery : PaginationQuery, IRequest<HomeBannerListDto>
    {
        
    }
}
