using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.User.Auth.Command.ForgotPassword
{
    public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandValidator()
        {
            RuleFor(x => x.Data.SalesCode).NotEmpty().WithMessage("Sales Code harus diisi");
            RuleFor(x => x.Data.Type).Must(type =>
            {
                return type == "email" || type == "phone";
            });
            RuleFor(x => x.Data.Identifier).NotEmpty().WithMessage("Email/Nomor Telepon harus diisi");
            RuleFor(x => x.Data.Identifier).EmailAddress().When(payload => payload.Data.Type == "email");
        }
    }
}
