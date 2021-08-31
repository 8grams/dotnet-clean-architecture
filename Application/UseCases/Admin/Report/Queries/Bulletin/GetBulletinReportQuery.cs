using System;
using System.Collections.Generic;
using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Report.Queries.Bulletin
{
    public class GetBulletinReportQuery : IRequest<GetBulletinReportDto>
    {
        public IList<FilterParams> Filters { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
    }
}
