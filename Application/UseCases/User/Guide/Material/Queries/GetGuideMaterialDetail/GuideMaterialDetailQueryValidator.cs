using System;
using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.User.Guide.Material.Queries.GetGuideMaterialDetail
{
    public class GuideMaterialDetailQueryValidator : AbstractValidator<GuideMaterialDetailQuery>
    {
        public GuideMaterialDetailQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty().GreaterThan(0);
        }
    }
}
