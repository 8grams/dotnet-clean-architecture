using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.AdminProfile.Command.CreateAdmin
{
    public class CreateAdminCommandValidator : AbstractValidator<CreateAdminCommand>
    {
        public CreateAdminCommandValidator()
        {
            RuleFor(x => x.Data.Name)
                .NotEmpty()
                .WithMessage("Nama harus diisi");

            RuleFor(x => x.Data.Email)
                .NotEmpty()
                .WithMessage("Email harus disertakan");

            RuleFor(x => x.Data.Password)
                .NotEmpty()
                .WithMessage("Password disertakan");

            RuleFor(x => x.Data.Phone)
                .NotEmpty()
                .WithMessage("No. Handphone harus disertakan");

            RuleFor(x => x.Data.RoleId)
                .NotEmpty()
                .WithMessage("Role harus disertakan");
        }
    }
}