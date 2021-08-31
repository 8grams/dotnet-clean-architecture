using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Training.Category.Queries.GetTrainingCategoryDetail
{
    public class TrainingCategoryDetailQuery : BaseQueryCommand, IRequest<TrainingCategoryDetailDto>
    {
        public int Id { set; get; }
    }
}
