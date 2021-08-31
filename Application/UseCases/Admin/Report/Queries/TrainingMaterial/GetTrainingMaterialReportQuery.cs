using System;
using System.Collections.Generic;
using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Report.Queries.TrainingMaterial
{
    public class GetTrainingMaterialReportQuery : AdminPaginationQuery, IRequest<GetTrainingMaterialReportDto>
    {
        public IList<FilterParams> Filters { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
    }
}
