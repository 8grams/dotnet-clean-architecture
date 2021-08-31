using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.User.Setting.Command.OTP
{
    public class OTPCommandValidator : AbstractValidator<OTPCommand>
    {
        public OTPCommandValidator()
        {
            RuleFor(x => x.Data.Pin)
                .NotEmpty()
                .WithMessage("PIN harus diisi");;
        }
    }
}
