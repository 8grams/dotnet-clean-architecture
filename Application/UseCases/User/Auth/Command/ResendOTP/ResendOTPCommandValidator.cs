using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.User.Auth.Command.ResendOTP
{
    public class ResendOTPCommandValidator : AbstractValidator<ResendOTPCommand>
    {
        public ResendOTPCommandValidator()
        {
            RuleFor(v => v.Data.Concern)
                .NotEmpty()
                .WithMessage("Concern harus diisi")
                .Must(concern =>
                {
                    return concern == "register" || concern == "update";
                });
            
            RuleFor(v => v.Data.UserId)
                .NotEmpty()
                .WithMessage("User ID harus diisi");
        }
    }
}
