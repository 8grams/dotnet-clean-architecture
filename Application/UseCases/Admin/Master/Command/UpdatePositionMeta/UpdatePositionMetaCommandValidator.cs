using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.Master.Command.UpdatePositionMeta
{
    public class UpdatePositionMetaCommandValidator : AbstractValidator<UpdatePositionMetaCommand>
    {
        public UpdatePositionMetaCommandValidator()
        {
            RuleFor(x => x.Data.Code)
                .NotEmpty()
                .WithMessage("Code harus diisi");

            RuleFor(x => x.Data.Description)
                .NotEmpty()
                .WithMessage("Deskripsi harus diisi");
        }
    }
}