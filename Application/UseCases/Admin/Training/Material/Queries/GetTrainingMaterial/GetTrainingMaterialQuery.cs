using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Training.Material.Queries.GetTrainingMaterial
{
    public class GetTrainingMaterialQuery : BaseAdminQueryCommand, IRequest<GetTrainingMaterialDto>
    {
        public int Id { set; get; }
    }
}
