using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.User.UserProfile.Queries.GetProfile
{
    public class GetUserQueryValidator : AbstractValidator<GetProfileQuery>
    {
        public GetUserQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty().GreaterThan(0);
        }
    }
}
