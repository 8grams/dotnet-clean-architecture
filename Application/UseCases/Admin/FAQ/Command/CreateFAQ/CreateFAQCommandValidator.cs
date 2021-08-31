using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.FAQ.Command.CreateFAQ
{
    public class CreateFAQCommandValidator : AbstractValidator<CreateFAQCommand>
    {
        public CreateFAQCommandValidator()
        {
            RuleFor(x => x.Data.Question)
                .NotEmpty()
                .WithMessage("Pertanyaan harus diisi");
            
            RuleFor(x => x.Data.Answer)
                .NotEmpty()
                .WithMessage("Jawaban harus diisi");
        }
    }
}