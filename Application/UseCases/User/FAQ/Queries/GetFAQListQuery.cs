using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.FAQ.Queries.GetFAQList
{
    public class GetFAQListQuery : BaseQueryCommand, IRequest<GetFAQListDto>
    {
        
    }
}
