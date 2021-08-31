using System;
using SFIDWebAPI.Application.UseCases.User.Training.Material.Models;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Training.Material.Queries.GetTrainingMaterialDetail
{
    public class TrainingMaterialDetailDto : BaseDto
    {
        public TrainingMaterialData Data { set; get; }
    }
}
