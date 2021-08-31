using System.Collections.Generic;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.UseCases.Admin.Training.Material.Models;

namespace SFIDWebAPI.Application.UseCases.Admin.Training.Material.Queries.GetTrainingMaterials
{
    public class GetTrainingMaterialsDto : PaginationDto
    {
        public IList<TrainingMaterialData> Data { set; get; }
    }   
}
