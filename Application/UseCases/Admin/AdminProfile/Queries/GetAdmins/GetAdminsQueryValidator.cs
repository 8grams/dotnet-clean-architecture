using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.AdminProfile.Queries.GetAdmins
{
    public class GetAdminQueryValidator : AbstractValidator<GetAdminsQuery>
    {
        public GetAdminQueryValidator()
        {
        }
    }
}