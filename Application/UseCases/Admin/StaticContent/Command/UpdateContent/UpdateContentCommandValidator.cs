using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.StaticContent.Command.UpdateContent
{
    public class UpdateContentCommandValidator : AbstractValidator<UpdateContentCommand>
    {
        public UpdateContentCommandValidator()
        {
            RuleFor(x => x.Data.Name)
                .NotEmpty()
                .WithMessage("Nama harus diisi");
            
            RuleFor(x => x.Data.Content)
                .NotEmpty()
                .WithMessage("Konten harus diisi");
        }
    }
}