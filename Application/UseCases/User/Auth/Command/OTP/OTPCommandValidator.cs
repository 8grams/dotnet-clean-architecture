using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.User.Auth.Command.OTP
{
    public class OTPCommandValidator : AbstractValidator<OTPCommand>
    {
        public OTPCommandValidator()
        {
            RuleFor(x => x.Data.UserId).NotEmpty();
            RuleFor(x => x.Data.Pin).NotEmpty().Length(6).WithMessage("Pin harus diisi dengan 6 karakter");
        }
    }
}
