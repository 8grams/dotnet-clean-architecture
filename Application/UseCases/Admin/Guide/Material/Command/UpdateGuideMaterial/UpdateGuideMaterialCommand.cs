using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Guide.Material.Command.UpdateGuideMaterial
{
    public class UpdateGuideMaterialCommand : BaseAdminQueryCommand, IRequest<UpdateGuideMaterialDto>
    {
        public UpdateGuideMaterialData Data { set; get; }
    }

    public class UpdateGuideMaterialData
    {
        public int Id { set; get; }
        public string Title { set; get; }
        public int ImageCoverId { set; get; }
        public int ImageThumbnailId { set; get; }
        public string FileCode { set; get; }
        public string FileByte { set; get; }
        public string FileName { set; get; }
        public bool IsRecommended { set; get; }
    }
}
