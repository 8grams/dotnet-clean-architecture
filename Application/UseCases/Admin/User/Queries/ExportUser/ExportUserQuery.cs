using System.Collections.Generic;
using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.User.Queries.ExportUser
{
    public class ExportUserQuery : IRequest<ExportUserDto>
    {
        public IList<FilterParams> Filters { set; get; }
        public string QuerySearch { set; get; }
    }
}
