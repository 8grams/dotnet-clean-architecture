using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;

namespace SFIDWebAPI.Application.UseCases.Admin.StaticContent.Command.UpdateContent
{
    public class UpdateContentCommandHandler : IRequestHandler<UpdateContentCommand, UpdateContentDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IUploader _uploader;

        public UpdateContentCommandHandler(ISFDDBContext context, IUploader uplo_uploader)
        {
            _context = context;
            _uploader = uplo_uploader;
        }

        public async Task<UpdateContentDto> Handle(UpdateContentCommand request, CancellationToken cancellationToken)
        {
            var content = await _context.StaticContent.FirstAsync();
            var isUpdate = content != null;

            if (!isUpdate) content = new Domain.Entities.StaticContent();
            
            switch (request.Data.Name)
            {
                case Domain.Entities.StaticContent.TYPE_APP_INFO:
                    content.AppInfo = request.Data.Content;
                    break;
                case Domain.Entities.StaticContent.TYPE_DISCLAIMER:
                    content.Disclaimer = request.Data.Content;
                    break;
                case Domain.Entities.StaticContent.TYPE_PRIVACY_POLICY:
                    content.PrivacyPolicy = request.Data.Content;
                    break;
                case Domain.Entities.StaticContent.TYPE_TERM_CONDITION:
                    content.TermCondition = request.Data.Content;
                    break;
                case Domain.Entities.StaticContent.TYPE_TUTORIAL:
                    content.Tutorial = await _uploader.UploadFile(request.Data.Content, "misc", "tutorial.pdf");
                    break;
                default:
                    throw new InvalidOperationException("Cannot process");
            }

            // create new
            if (isUpdate)
            {
                _context.StaticContent.Update(content);
            }
            else
            {
                _context.StaticContent.Add(content);
            }
            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateContentDto
            {
                Success = true,
                Message = "Static Content has been successfully updated"
            };
        }
    }
}