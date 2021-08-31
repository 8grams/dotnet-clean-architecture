using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.User.PKT.Report.Command.SendReport
{
    public class SendReportCommandValidator : AbstractValidator<SendReportCommand>
    {
        public SendReportCommandValidator()
        {
            RuleFor(v => v.Data.Vin)
                .NotEmpty();
            RuleFor(v => v.Data.Image)
                .NotEmpty();
            RuleFor(v => v.Data.Type)
                .NotEmpty();
        }
    }
}
