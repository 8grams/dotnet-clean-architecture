using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.StaticContent.Queries.GetContent
{
    public class GetContentQueryValidator : AbstractValidator<GetContentQuery>
    {
        public GetContentQueryValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty();
        }
    }
}
