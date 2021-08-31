using MediatR;
using System.Collections.Generic;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Training.Material.Queries.GetTrainingMaterialList
{
    public class TrainingMaterialListQuery : PaginationQuery, IRequest<TrainingMaterialListDto>
    {
        public IList<string> Includes { set; get; }
        public IList<FilterParams> Filters { set; get; }
    }
}
