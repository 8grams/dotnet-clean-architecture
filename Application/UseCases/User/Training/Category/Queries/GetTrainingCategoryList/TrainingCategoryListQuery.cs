using System.Collections.Generic;
using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Training.Category.Queries.GetTrainingCategoryList
{
    public class TrainingCategoryListQuery : PaginationQuery, IRequest<TrainingCategoryListDto>
    {
        public IList<string> Includes { set; get; }
    }
}
