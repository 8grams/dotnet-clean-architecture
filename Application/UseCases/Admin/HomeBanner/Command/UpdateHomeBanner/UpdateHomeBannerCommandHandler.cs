using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Exceptions;

namespace SFIDWebAPI.Application.UseCases.Admin.HomeBanner.Command.UpdateHomeBanner
{
    public class UpdateHomeBannerCommandHandler : IRequestHandler<UpdateHomeBannerCommand, UpdateHomeBannerDto>
    {
        private readonly ISFDDBContext _context;

        public UpdateHomeBannerCommandHandler(ISFDDBContext context)
        {
            _context = context;
        }

        public async Task<UpdateHomeBannerDto> Handle(UpdateHomeBannerCommand request, CancellationToken cancellationToken)
        {
            var banner = await _context.HomeBanners.FindAsync(request.Data.Id);
            if (banner == null) throw new NotFoundException(nameof(Domain.Entities.HomeBanner), request.Data.Id);

            banner.Name = request.Data.Name;
            banner.ImageId = request.Data.ImageId;
            banner.LastUpdateBy = request.AdminName;
            
            _context.HomeBanners.Update(banner);
            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateHomeBannerDto
            {
                Success = true,
                Message = "Home Banner has been successfully updated"
            };
        }
    }
}