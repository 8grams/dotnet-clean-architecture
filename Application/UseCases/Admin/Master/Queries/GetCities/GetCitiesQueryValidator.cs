using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.Master.Queries.GetCities
{
    public class GetCitiesQueryValidator : AbstractValidator<GetCitiesQuery>
    {
        public GetCitiesQueryValidator()
        {
            RuleFor(v => v.QuerySearch)
                .MinimumLength(3)
                .NotEmpty();
        }
    }
}
