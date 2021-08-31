using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.AdminProfile.Command.UpdateAdmin
{
    public class UpdateAdminCommandValidator : AbstractValidator<UpdateAdminCommand>
    {
        public UpdateAdminCommandValidator()
        {
            RuleFor(x => x.Data.Name)
                .NotEmpty()
                .WithMessage("Nama harus diisi");

            RuleFor(x => x.Data.Email)
                .NotEmpty()
                .WithMessage("Email harus disertakan");

            RuleFor(x => x.Data.Phone)
                .NotEmpty()
                .WithMessage("No. Handphone harus disertakan");

            RuleFor(x => x.Data.RoleId)
                .NotEmpty()
                .WithMessage("Role harus disertakan");
        }
    }
}