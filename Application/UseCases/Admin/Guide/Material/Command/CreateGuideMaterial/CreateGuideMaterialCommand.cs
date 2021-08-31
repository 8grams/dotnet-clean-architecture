using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Guide.Material.Command.CreateGuideMaterial
{
    public class CreateGuideMaterialCommand : BaseAdminQueryCommand, IRequest<CreateGuideMaterialDto>
    {
        public CreateGuideMaterialData Data { set; get; }
    }

    public class CreateGuideMaterialData
    {
        public string Title { set; get; }
        public int ImageThumbnailId { set; get; }
        public string FileCode { set; get; }
        public string FileByte { set; get; }
        public string FileName { set; get; }
        public bool IsRecommended { set; get; }
        public int CategoryId { set; get; }
    }
}
