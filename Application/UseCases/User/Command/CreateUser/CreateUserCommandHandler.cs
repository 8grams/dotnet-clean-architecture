using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebApi.Application.Interfaces;

namespace WebApi.Application.UseCases.User.Command.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserDto>
    {
        private readonly IWebApiDbContext _context;
        private readonly IUploader _uploader;
        private readonly IMediator _mediator;
        private readonly IMemoryData _memoryData;

        public CreateUserCommandHandler(IWebApiDbContext context, IMediator mediator, IUploader uploader, IMemoryData memoryData)
        {
            _context = context;
            _uploader = uploader;
            _mediator = mediator;
            _memoryData = memoryData;
        }

        public async Task<CreateUserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            var fileUrl = await _uploader.UploadFile(request.Data.FileByte, "avatar", request.Data.UserName);

            var user = new Domain.Entities.User {
                Name = request.Data.Name,
                UserName = request.Data.UserName,
                Email = request.Data.Email,
                Phone = request.Data.Phone,
                Age = request.Data.Age,
                ProfilePicture = fileUrl,
                CreatedBy = "system"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateUserDto
            {
                Success = true,
                Message = "User has been successfully created"
            };
        }
    }
}