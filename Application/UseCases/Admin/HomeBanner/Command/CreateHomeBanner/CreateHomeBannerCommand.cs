using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.HomeBanner.Command.CreateHomeBanner
{
    public class CreateHomeBannerCommand : BaseAdminQueryCommand, IRequest<CreateHomeBannerDto>
    {
        public CreateHomeBannerData Data { set; get; }
    }

    public class CreateHomeBannerData
    {
        public string Name { set; get; }
        public int ImageId { set; get; }
    }
}
