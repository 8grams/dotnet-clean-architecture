using System;
using MediatR;
using System.Collections.Generic;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Report.Queries.ExportInfo
{
    public class ExportInfoQuery : IRequest<ExportInfoDto>
    {
        public IList<FilterParams> Filters { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
    }
}
