using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.MasterCar.Command.UpdateMasterCar
{
    public class UpdateMasterCarCommand : BaseAdminQueryCommand, IRequest<UpdateMasterCarDto>
    {
        public UpdateMasterCarData Data { set; get; }
    }

    public class UpdateMasterCarData
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Tag { set; get; }
        public bool TrainingActive { set; get; }
        public bool GuideActive { set; get; }
        public int ImageCoverId { set; get; }
        public int ImageThumbnailId { set; get; }
        public int ImageLogoId { set; get; }
    }
}
