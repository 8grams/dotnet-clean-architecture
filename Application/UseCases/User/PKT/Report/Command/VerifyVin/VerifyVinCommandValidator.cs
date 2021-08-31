using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.User.PKT.Report.Command.VerifyVin
{
    public class VerifyVinCommandValidator : AbstractValidator<VerifyVinCommand>
    {
        public VerifyVinCommandValidator()
        {
            RuleFor(v => v.Data.Vin)
                .NotEmpty();
            RuleFor(v => v.Data.Type)
                .NotEmpty();
        }
    }
}
