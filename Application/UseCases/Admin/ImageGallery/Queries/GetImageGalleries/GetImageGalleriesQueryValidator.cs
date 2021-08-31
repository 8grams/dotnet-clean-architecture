using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.ImageGallery.Queries.GetImageGalleries
{
    public class GetImageGalleriesQueryValidator : AbstractValidator<GetImageGalleriesQuery>
    {
        public GetImageGalleriesQueryValidator()
        {
            RuleFor(v => v.PagingPage)
                .GreaterThan(0);

            RuleFor(v => v.PagingLimit)
                .GreaterThan(0);

            RuleFor(v => v.SortColumn)
                .NotEmpty();

            RuleFor(v => v.SortType)
                .NotEmpty();
        }
    }
}
