using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.UseCases.Admin.Training.Material.Models;

namespace SFIDWebAPI.Application.UseCases.Admin.Training.Material.Queries.GetTrainingMaterial
{
    public class GetTrainingMaterialDto : BaseDto
    {
        public TrainingMaterialData Data { set; get; }
    }   
}
