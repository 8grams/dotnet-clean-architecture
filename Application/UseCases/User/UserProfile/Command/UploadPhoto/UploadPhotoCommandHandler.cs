using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.UserProfile.Command.UploadPhoto
{
    public class UploadPhotoCommandHandler : IRequestHandler<UploadPhotoCommand, UploadPhotoDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IUploader _uploader;
        private readonly IMapper _mapper;

        public UploadPhotoCommandHandler(ISFDDBContext context, IUploader uploader, IMapper mapper)
        {
            _context = context;
            _uploader = uploader;
            _mapper = mapper;
        }

        public async Task<UploadPhotoDto> Handle(UploadPhotoCommand request, CancellationToken cancellationToken)
        {
            var profile = await _context.Users.FindAsync(request.UserId);
            var fileUrl = await _uploader.UploadFile(request.Data.FileByte, "profile", "profile-photo.png");

            profile.ProfilePhoto = fileUrl;
            await _context.SaveChangesAsync(cancellationToken);

            return new UploadPhotoDto
            {
                Success = true,
                Message = "Profile Photo has been successfully uploaded",
                Data = _mapper.Map<ProfileDto>(profile)
            };
        }
    }
}