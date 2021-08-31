using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.Report.Queries.Dashboard
{
    public class GetDashboardQueryValidator : AbstractValidator<GetDashboardQuery>
    {
        public GetDashboardQueryValidator()
        {
            // RuleFor(v => v.DealerGroupId)
            //     .NotEmpty();

            // RuleFor(v => v.StartDate)
            //     .NotEmpty();

            // RuleFor(v => v.EndDate)
            //     .NotEmpty();
        }
    }
}
