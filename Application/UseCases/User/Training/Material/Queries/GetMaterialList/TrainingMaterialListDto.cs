using System;
using System.Collections.Generic;
using SFIDWebAPI.Application.UseCases.User.Training.Material.Models;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Training.Material.Queries.GetTrainingMaterialList
{
    public class TrainingMaterialListDto : PaginationDto
    {
        public IEnumerable<TrainingMaterialData> Data { set; get; }
    }
}
