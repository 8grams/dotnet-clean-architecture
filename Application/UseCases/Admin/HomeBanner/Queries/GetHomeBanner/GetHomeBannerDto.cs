using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.UseCases.Admin.HomeBanner.Models;

namespace SFIDWebAPI.Application.UseCases.Admin.HomeBanner.Queries.GetHomeBanner
{
    public class GetHomeBannerDto : BaseDto
    {
        public HomeBannerData Data { set; get; }
    }   
}
