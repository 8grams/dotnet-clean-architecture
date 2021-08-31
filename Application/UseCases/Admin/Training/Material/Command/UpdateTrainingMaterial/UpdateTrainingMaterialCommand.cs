using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Training.Material.Command.UpdateTrainingMaterial
{
    public class UpdateTrainingMaterialCommand : BaseAdminQueryCommand, IRequest<UpdateTrainingMaterialDto>
    {
        public UpdateTrainingMaterialData Data { set; get; }
    }

    public class UpdateTrainingMaterialData
    {
        public int Id { set; get; }
        public string Title { set; get; }
        public string Content { set; get; }
        public int ImageThumbnailId { set; get; }
        public string FileCode { set; get; }
        public string FileByte { set; get; }
        public string FileName { set; get; }
        public bool IsRecommended { set; get; }
    }
}
