using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.ImageGallery.Queries.GetImageGallery
{
    public class GetImageGalleryQueryValidator : AbstractValidator<GetImageGalleryQuery>
    {
        public GetImageGalleryQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty().GreaterThan(0).WithMessage("ID harus terdaftar");
        }
    }
}
