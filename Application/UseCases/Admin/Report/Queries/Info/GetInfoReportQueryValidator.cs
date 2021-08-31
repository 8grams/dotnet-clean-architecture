using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.Report.Queries.Info
{
    public class GetInfoReportQueryValidator : AbstractValidator<GetInfoReportQuery>
    {
        public GetInfoReportQueryValidator()
        {
        }
    }
}
