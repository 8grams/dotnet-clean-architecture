using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Exceptions;

namespace SFIDWebAPI.Application.UseCases.Admin.MasterCar.Command.UpdateMasterCar
{
    public class UpdateMasterCarCommandHandler : IRequestHandler<UpdateMasterCarCommand, UpdateMasterCarDto>
    {
        private readonly ISFDDBContext _context;

        public UpdateMasterCarCommandHandler(ISFDDBContext context)
        {
            _context = context;
        }

        public async Task<UpdateMasterCarDto> Handle(UpdateMasterCarCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.MasterCars.FindAsync(request.Data.Id);
            if (category == null) throw new NotFoundException(nameof(Domain.Entities.MasterCar), request.Data.Id);

            category.Title = request.Data.Name;
            category.Tag = request.Data.Tag;
            category.TrainingActive = request.Data.TrainingActive;
            category.GuideActive = request.Data.GuideActive;
            category.ImageCoverId = request.Data.ImageCoverId;
            category.ImageThumbnailId = request.Data.ImageThumbnailId;
            category.ImageLogoId = request.Data.ImageLogoId;
            category.LastUpdateBy = request.AdminName;
            
            _context.MasterCars.Update(category);
            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateMasterCarDto
            {
                Success = true,
                Message = "Training Category has been successfully updated"
            };
        }
    }
}