using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Guide.Material.Queries.GetGuideMaterialDetail
{
    public class GuideMaterialDetailQuery : BaseQueryCommand, IRequest<GuideMaterialDetailDto>
    {
        public int Id { set; get; }
    }
}
