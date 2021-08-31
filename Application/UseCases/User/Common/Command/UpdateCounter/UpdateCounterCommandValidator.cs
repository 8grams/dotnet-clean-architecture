using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.User.Common.Command.UpdateCounter
{
    public class UpdateCounterCommandValidator : AbstractValidator<UpdateCounterCommand>
    {
        public UpdateCounterCommandValidator()
        {
            RuleFor(v => v.Data.FileId).NotEmpty().WithMessage("File ID harus diisi");
            RuleFor(v => v.Data.FileType).NotEmpty().WithMessage("File Type harus diisi");
        }
    }
}
