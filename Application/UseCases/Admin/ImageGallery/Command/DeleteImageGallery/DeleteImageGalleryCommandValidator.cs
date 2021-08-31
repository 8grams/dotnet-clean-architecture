using System.Linq;
using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.ImageGallery.Command.DeleteImageGallery
{
    public class DeleteImageGalleryCommandValidator : AbstractValidator<DeleteImageGalleryCommand>
    {
        public DeleteImageGalleryCommandValidator()
        {
            RuleFor(x => x.Ids)
                .Must(e => e.All(item => item.HasValue))
                .WithMessage("Id harus diisi");
        }
    }
}