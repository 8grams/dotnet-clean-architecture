using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFIDWebAPI.Application.Interfaces;

namespace SFIDWebAPI.Application.UseCases.Admin.MasterCar.Command.CreateMasterCar
{
    public class CreateMasterCarCommandHandler : IRequestHandler<CreateMasterCarCommand, CreateMasterCarDto>
    {
        private readonly ISFDDBContext _context;

        public CreateMasterCarCommandHandler(ISFDDBContext context)
        {
            _context = context;
        }

        public async Task<CreateMasterCarDto> Handle(CreateMasterCarCommand request, CancellationToken cancellationToken)
        {
            _context.MasterCars.Add(new Domain.Entities.MasterCar
            {
                Title = request.Data.Name,
                Tag = request.Data.Tag,
                TrainingActive = request.Data.IsActive,
                GuideActive = request.Data.IsActive,
                ImageCoverId = request.Data.ImageCoverId,
                ImageThumbnailId = request.Data.ImageThumbnailId,
                ImageLogoId = request.Data.ImageLogoId,
                CreateBy = request.AdminName
            });

            await _context.SaveChangesAsync(cancellationToken);

            return new CreateMasterCarDto
            {
                Success = true,
                Message = "Training Category has been successfully created"
            };
        }
    }
}