using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Training.Material.Command.CreateTrainingMaterial
{
    public class CreateTrainingMaterialCommand : BaseAdminQueryCommand, IRequest<CreateTrainingMaterialDto>
    {
        public CreateTrainingMaterialData Data { set; get; }
    }

    public class CreateTrainingMaterialData
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
