using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFIDWebAPI.Application.Interfaces;

namespace SFIDWebAPI.Application.UseCases.Admin.HomeBanner.Command.CreateHomeBanner
{
    public class CreateHomeBannerCommandHandler : IRequestHandler<CreateHomeBannerCommand, CreateHomeBannerDto>
    {
        private readonly ISFDDBContext _context;

        public CreateHomeBannerCommandHandler(ISFDDBContext context)
        {
            _context = context;
        }

        public async Task<CreateHomeBannerDto> Handle(CreateHomeBannerCommand request, CancellationToken cancellationToken)
        {
            var homeBanner = new Domain.Entities.HomeBanner
            {
                Name = request.Data.Name,
                ImageId = request.Data.ImageId,
                CreateBy = request.AdminName,
                PublishedAt = DateTime.Now,
                ExpiresAt = DateTime.Now.AddYears(1)
            };

            _context.HomeBanners.Add(homeBanner);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateHomeBannerDto
            {
                Success = true,
                Message = "Home Banner has been successfully created"
            };
        }
    }
}