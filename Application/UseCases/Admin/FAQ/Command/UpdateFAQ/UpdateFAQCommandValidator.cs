using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.FAQ.Command.UpdateFAQ
{
    public class UpdateFAQCommandValidator : AbstractValidator<UpdateFAQCommand>
    {
        public UpdateFAQCommandValidator()
        {
            RuleFor(x => x.Data.Question)
                .NotEmpty()
                .WithMessage("Pertanyaan harus diisi");
            
            RuleFor(x => x.Data.Answer)
                .NotEmpty()
                .WithMessage("Jawaban harus diis");
        }
    }
}