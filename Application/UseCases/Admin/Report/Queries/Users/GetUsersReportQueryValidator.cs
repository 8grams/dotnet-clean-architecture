using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.Report.Queries.Users
{
    public class GetUsersReportQueryValidator : AbstractValidator<GetUsersReportQuery>
    {
        public GetUsersReportQueryValidator()
        {
        }
    }
}
