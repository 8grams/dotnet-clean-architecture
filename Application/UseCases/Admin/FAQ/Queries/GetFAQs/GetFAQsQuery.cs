using MediatR;
using System.Collections.Generic;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.FAQ.Queries.GetFAQs
{
    public class GetFAQsQuery : AdminPaginationQuery, IRequest<GetFAQsDto>
    {
    }
}
