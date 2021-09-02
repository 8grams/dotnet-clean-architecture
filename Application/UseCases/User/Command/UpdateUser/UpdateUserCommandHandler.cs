using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebApi.Application.Interfaces;
using WebApi.Application.Exceptions;

namespace WebApi.Application.UseCases.User.Command.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserDto>
    {
        private readonly IWebApiDbContext _context;
        private readonly IUploader _uploader;

        public UpdateUserCommandHandler(IWebApiDbContext context, IUploader uploader)
        {
            _context = context;
            _uploader = uploader;
        }

        public async Task<UpdateUserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.Data.Id);
            if (user == null) throw new NotFoundException(nameof(Domain.Entities.User), request.Data.Id);

            var fileUrl = await _uploader.UploadFile(request.Data.FileByte, "avatar", request.Data.UserName);

            user.Name = request.Data.Name;
            user.UserName = request.Data.UserName;
            user.Email = request.Data.Email;
            user.Age = request.Data.Age;
            user.ProfilePicture = fileUrl;
            user.LastUpdatedBy = "system";

            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateUserDto
            {
                Success = true,
                Message = "User has been successfully updated"
            };
        }
    }
}