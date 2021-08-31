using System;
using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.User.StaticContent.Queries.GetStaticContent
{
    public class StaticContentQueryValidator : AbstractValidator<StaticContentQuery>
    {
        public StaticContentQueryValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .NotNull()
                .Must(e => e.Equals("appinfo") 
                    || e.Equals("privacy") 
                    || e.Equals("term") 
                    || e.Equals("tutorial") 
                    || e.Equals("disclaimer"));
        }
    }
}
