using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.HomeBanner.Command.UpdateHomeBanner
{
    public class UpdateHomeBannerCommand : BaseAdminQueryCommand, IRequest<UpdateHomeBannerDto>
    {
        public UpdateHomeBannerData Data { set; get; }
    }

    public class UpdateHomeBannerData
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public int ImageId { set; get; }
    }
}
