using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Training.Material.Queries.GetTrainingMaterialDetail
{
    public class TrainingMaterialDetailQuery : BaseQueryCommand, IRequest<TrainingMaterialDetailDto>
    {
        public int Id { set; get; }
    }
}
