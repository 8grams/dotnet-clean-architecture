using System;
using MediatR;
using System.Collections.Generic;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Report.Queries.ExportTrainingMaterial
{
    public class ExportTrainingMaterialQuery : IRequest<ExportTrainingMaterialDto>
    {
        public IList<FilterParams> Filters { set; get; }
        public int MasterCarId { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
    }
}
