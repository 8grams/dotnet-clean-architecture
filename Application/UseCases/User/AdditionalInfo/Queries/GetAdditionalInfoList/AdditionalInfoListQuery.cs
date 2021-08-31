using MediatR;
using System.Collections.Generic;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.AdditionalInfo.Queries.GetAdditionalInfoList
{
    public class AdditionalInfoListQuery : PaginationQuery, IRequest<AdditionalInfoListDto>
    {
        
    }
}
