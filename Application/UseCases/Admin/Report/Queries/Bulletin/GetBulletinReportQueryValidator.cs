using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.Report.Queries.Bulletin
{
    public class GetBulletinReportQueryValidator : AbstractValidator<GetBulletinReportQuery>
    {
        public GetBulletinReportQueryValidator()
        {
        }
    }
}
