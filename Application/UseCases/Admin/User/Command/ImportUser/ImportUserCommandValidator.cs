using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.User.Command.ImportUser
{
    public class ImportUserCommandValidator : AbstractValidator<ImportUserCommand>
    {
        public ImportUserCommandValidator()
        {
            RuleFor(x => x.Data.FileName)
                .NotEmpty()
                .WithMessage("Nama File harus diisi");

            RuleFor(x => x.Data.FileByte)
                .NotEmpty()
                .WithMessage("File harus disertakan");
        }
    }
}