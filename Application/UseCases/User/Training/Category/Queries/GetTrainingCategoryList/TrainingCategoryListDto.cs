using System.Collections.Generic;
using SFIDWebAPI.Application.UseCases.User.Training.Category.Models;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Training.Category.Queries.GetTrainingCategoryList
{
    public class TrainingCategoryListDto : PaginationDto
    {
        public IList<TrainingCategoryData> Data { set; get; }
    }
}
