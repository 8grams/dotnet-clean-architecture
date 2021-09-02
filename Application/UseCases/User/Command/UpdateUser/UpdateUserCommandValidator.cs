using FluentValidation;

namespace WebApi.Application.UseCases.User.Command.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Data.Name)
                .NotEmpty()
                .WithMessage("Nama harus diisi");

            RuleFor(x => x.Data.UserName)
                .NotEmpty()
                .WithMessage("Username harus disertakan");

            RuleFor(x => x.Data.Email)
                .NotEmpty()
                .WithMessage("Email Name harus disertakan");

            RuleFor(x => x.Data.Phone)
                .NotEmpty()
                .WithMessage("Nomor Telp harus disertakan");

            RuleFor(x => x.Data.Age)
                .NotEmpty()
                .WithMessage("Umur harus disertakan");

            RuleFor(x => x.Data.FileByte)
                .NotEmpty()
                .WithMessage("File harus disertakan");
        }
    }
}