using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFIDWebAPI.Application.Interfaces;

namespace SFIDWebAPI.Application.UseCases.Admin.HomeBanner.Command.DeleteHomeBanner
{
    public class DeleteHomeBannerCommandHandler : IRequestHandler<DeleteHomeBannerCommand, DeleteHomeBannerDto>
    {
        private readonly ISFDDBContext _context;

        public DeleteHomeBannerCommandHandler(ISFDDBContext context)
        {
            _context = context;
        }

        public async Task<DeleteHomeBannerDto> Handle(DeleteHomeBannerCommand request, CancellationToken cancellationToken)
        {
            foreach (var id in request.Ids)
            {
                var banner = await _context.HomeBanners.FindAsync(id);
                if (banner != null) _context.HomeBanners.Remove(banner);
            }

            await _context.SaveChangesAsync(cancellationToken);
            
            return new DeleteHomeBannerDto
            {
                Success = true,
                Message = "Home Banner has been successfully deleted"
            };
        }
    }


}