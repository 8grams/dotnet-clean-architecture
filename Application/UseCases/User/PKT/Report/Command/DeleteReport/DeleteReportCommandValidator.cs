using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.User.PKT.Report.Command.DeleteReport
{
    public class DeleteReportCommandValidator : AbstractValidator<DeleteReportCommand>
    {
        public DeleteReportCommandValidator()
        {
            RuleFor(v => v.Data.Id)
                .NotEmpty();
        }
    }
}
