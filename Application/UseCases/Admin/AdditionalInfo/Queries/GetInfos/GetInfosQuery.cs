using MediatR;
using System.Collections.Generic;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.AdditionalInfo.Queries.GetInfos
{
    public class GetInfosQuery : AdminPaginationQuery, IRequest<GetInfosDto>
    {
        
    }
}
