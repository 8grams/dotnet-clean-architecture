using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.User.Auth.Command.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Data.Email).NotEmpty().EmailAddress().WithMessage("Email harus diisi dengan alamat email yang valid");
            RuleFor(x => x.Data.Phone).NotEmpty().WithMessage("Nomor Handphone harus diisi");
            RuleFor(x => x.Data.Password).NotEmpty().MinimumLength(6).WithMessage("Password harus diisi minimal 6 karakter");
            RuleFor(x => x.Data.SalesCode).NotEmpty().WithMessage("Sales Code harus diisi");
            RuleFor(x => x.Data.DeviceId).NotEmpty().WithMessage("Device ID tidak terdeteksi");

        }
    }
}
