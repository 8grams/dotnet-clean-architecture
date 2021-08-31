using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.UseCases.Admin.ImageGallery.Models;

namespace SFIDWebAPI.Application.UseCases.Admin.ImageGallery.Queries.GetImageGallery
{
    public class GetImageGalleryQueryHandler : IRequestHandler<GetImageGalleryQuery, GetImageGalleryDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public GetImageGalleryQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetImageGalleryDto> Handle(GetImageGalleryQuery request, CancellationToken cancellationToken)
        {
            var img = await _context.ImageGalleries.FindAsync(request.Id);

            var response = new GetImageGalleryDto()
            {
                Success = true,
                Message = "Image is succefully retrieved"
            };

            if (img != null)
            {
                response.Data = _mapper.Map<ImageGalleryData>(img);
            }
            else
            {
                response.Success = false;
                response.Message = "Image is not found";
            }

            return response;
        }
    }
}
