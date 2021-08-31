using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFIDWebAPI.Application.Interfaces;

namespace SFIDWebAPI.Application.UseCases.Admin.AdminProfile.Command.UpdateAdmin
{
    public class UpdateAdminCommandHandler : IRequestHandler<UpdateAdminCommand, UpdateAdminDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMediator _mediator;

        public UpdateAdminCommandHandler(ISFDDBContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<UpdateAdminDto> Handle(UpdateAdminCommand request, CancellationToken cancellationToken)
        {
            var admin = await _context.Admins.FindAsync(request.Data.Id);
            admin.Name = request.Data.Name;
            admin.Phone = request.Data.Phone;
            admin.Email = request.Data.Email;
            admin.RoleId = request.Data.RoleId;

            _context.Admins.Update(admin);
            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateAdminDto
            {
                Success = true,
                Message = "Admin has been successfully updated"
            };
        }
    }
}