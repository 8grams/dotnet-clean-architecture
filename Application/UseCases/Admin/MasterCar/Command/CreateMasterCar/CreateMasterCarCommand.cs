using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.MasterCar.Command.CreateMasterCar
{
    public class CreateMasterCarCommand : BaseAdminQueryCommand, IRequest<CreateMasterCarDto>
    {
        public CreateMasterCarData Data { set; get; }
    }

    public class CreateMasterCarData
    {
        public string Name { set; get; }
        public string Tag { set; get; }
        public bool IsActive { set; get; }
        public int ImageCoverId { set; get; }
        public int ImageThumbnailId { set; get; }
        public int ImageLogoId { set; get; }
    }
}
