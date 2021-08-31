using System.Collections.Generic;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.UseCases.Admin.HomeBanner.Models;

namespace SFIDWebAPI.Application.UseCases.Admin.HomeBanner.Queries.GetHomeBanners
{
    public class GetHomeBannersDto : PaginationDto
    {
        public IList<HomeBannerData> Data { set; get; }
    }   
}
