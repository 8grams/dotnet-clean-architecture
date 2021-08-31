using SFIDWebAPI.Application.UseCases.User.Training.Category.Models;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Training.Category.Queries.GetTrainingCategoryDetail
{
    public class TrainingCategoryDetailDto : BaseDto
    {
        public TrainingCategoryData Data { set; get; }
    }
}
