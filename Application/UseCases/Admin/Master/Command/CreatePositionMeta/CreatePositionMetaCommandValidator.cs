using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.Master.Command.CreatePositionMeta
{
    public class CreatePositionMetaCommandValidator : AbstractValidator<CreatePositionMetaCommand>
    {
        public CreatePositionMetaCommandValidator()
        {
            RuleFor(x => x.Data.Description)
                .NotEmpty()
                .WithMessage("Deskripsi harus diisi");
        }
    }
}