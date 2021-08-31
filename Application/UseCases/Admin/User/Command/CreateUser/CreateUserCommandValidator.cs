using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.User.Command.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Data.SalesmanCode)
                .NotEmpty()
                .WithMessage("Salesman Code harus diisi");

            RuleFor(x => x.Data.SalesmanName)
                .NotEmpty()
                .WithMessage("Salesman Name harus diisi");

            RuleFor(x => x.Data.Email)
                .NotEmpty()
                .WithMessage("Email harus diisi");

            RuleFor(x => x.Data.Phone)
                .NotEmpty()
                .WithMessage("Nomor Handphone harus diisi");

            RuleFor(x => x.Data.Password)
                .NotEmpty()
                .WithMessage("Password harus diisi");

            RuleFor(x => x.Data.Password)
                .Equal(x => x.Data.PasswordConfirmation)
                .WithMessage("Password harus cocok dengan konfirmasi password");
            
            RuleFor(x => x.Data.JoinDate)
                .NotEmpty()
                .WithMessage("Join Date harus diisi");

            RuleFor(x => x.Data.SupervisorName)
                .NotEmpty()
                .WithMessage("Supervisor Name harus diisi");

            RuleFor(x => x.Data.LevelId)
                .NotEmpty()
                .WithMessage("Level harus diisi");

            RuleFor(x => x.Data.LastYearGrade)
                .NotEmpty()
                .WithMessage("Grade tahun lalu harus diisi");

            RuleFor(x => x.Data.CurrentYearGrade)
                .NotEmpty()
                .WithMessage("Grade sekarang harus diisi");

            RuleFor(x => x.Data.DealerBranchId)
                .NotEmpty()
                .WithMessage("Dealer Branch harus diisi");
        }
    }
}