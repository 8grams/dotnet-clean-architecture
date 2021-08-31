using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Guide.Material.Queries.GetGuideMaterial
{
    public class GetGuideMaterialQuery : AdminPaginationQuery, IRequest<GetGuideMaterialDto>
    {
        public int Id { set; get; }
    }
}
