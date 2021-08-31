using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.MasterCar.Queries.GetMasterCars
{
    public class GetMasterCarsQueryValidator : AbstractValidator<GetMasterCarsQuery>
    {
        public GetMasterCarsQueryValidator()
        {
            RuleFor(v => v.PagingPage)
                .GreaterThan(0);

            RuleFor(v => v.PagingLimit)
                .GreaterThan(0);

            RuleFor(v => v.SortColumn)
                .NotEmpty();

            RuleFor(v => v.SortType)
                .NotEmpty();
        }
    }
}
