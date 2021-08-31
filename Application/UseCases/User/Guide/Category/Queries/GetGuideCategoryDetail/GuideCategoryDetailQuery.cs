using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Guide.Category.Queries.GetGuideCategoryDetail
{
    public class GuideCategoryDetailQuery : BaseQueryCommand, IRequest<GuideCategoryDetailDto>
    {
        public int Id { set; get; }
    }
}
