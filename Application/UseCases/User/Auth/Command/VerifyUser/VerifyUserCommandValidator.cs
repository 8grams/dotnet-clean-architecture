using FluentValidation;
namespace SFIDWebAPI.Application.UseCases.User.Auth.Command.VerifyUser
{
    public class VerifyUserCommandValidator : AbstractValidator<VerifyUserCommand>
    {
        public VerifyUserCommandValidator()
        {
            RuleFor(v => v.Data.SalesCode).NotEmpty().WithMessage("Sales Code harus diisi");
        }
    }
}
