using System;
using MediatR;
using System.Collections.Generic;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Report.Queries.ExportQuickGuide
{
    public class ExportQuickGuideQuery : IRequest<ExportQuickGuideDto>
    {
        public IList<FilterParams> Filters { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
    }
}
