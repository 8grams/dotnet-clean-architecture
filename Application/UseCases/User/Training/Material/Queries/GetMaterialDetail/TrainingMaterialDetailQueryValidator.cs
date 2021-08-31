using System;
using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.User.Training.Material.Queries.GetTrainingMaterialDetail
{
    public class TrainingMaterialDetailQueryValidator : AbstractValidator<TrainingMaterialDetailQuery>
    {
        public TrainingMaterialDetailQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty().GreaterThan(0);
        }
    }
}
