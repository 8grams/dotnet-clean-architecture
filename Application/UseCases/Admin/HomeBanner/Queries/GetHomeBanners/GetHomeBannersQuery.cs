using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.HomeBanner.Queries.GetHomeBanners
{
    public class GetHomeBannersQuery : AdminPaginationQuery, IRequest<GetHomeBannersDto>
    {
    }
}
