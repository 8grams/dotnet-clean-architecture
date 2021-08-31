using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Infrastructure.Persistences;

namespace SFIDWebAPI.Application.UseCases.Admin.AdminProfile.Command.CreateAdmin
{
    public class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand, CreateAdminDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMediator _mediator;

        public CreateAdminCommandHandler(ISFDDBContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<CreateAdminDto> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            _context.Admins.Add(new Domain.Entities.Admin {
                Name = request.Data.Name,
                Email = request.Data.Email,
                Password = DBUtil.PasswordHash(request.Data.Password),
                Phone = request.Data.Phone,
                RoleId = request.Data.RoleId
            });

            await _context.SaveChangesAsync(cancellationToken);

            return new CreateAdminDto
            {
                Success = true,
                Message = "Admin has been successfully created"
            };
        }
    }
}