using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;

namespace SFIDWebAPI.Application.UseCases.Admin.User.Command.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMediator _mediator;
        private readonly IUploader _uploader;

        public UpdateUserCommandHandler(ISFDDBContext context, IMediator mediator, IUploader uploader)
        {
            _context = context;
            _mediator = mediator;
            _uploader = uploader;
        }

        public async Task<UpdateUserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.Data.Id);
            var meta = await _context.SalesmenMeta.Where(e => e.SalesmanCode == user.UserName).FirstOrDefaultAsync();
            
            // we can update user detail
            if (meta != null)
            {
                meta.SalesmanName = request.Data.SalesmanName;
                meta.SalesmanHireDate = request.Data.JoinDate;
                meta.SuperiorName = request.Data.SupervisorName;
                meta.SalesmanEmail = request.Data.Email;
                meta.SalesmanHandphone = request.Data.Phone;
                meta.DealerBranchId = request.Data.DealerBranchId;
                meta.JobPositionId = request.Data.JobPositionId;
                meta.SalesmanLevelId = request.Data.LevelId;
                meta.GradeCurrentYear = (Int16) request.Data.CurrentYearGrade;
                meta.GradeLastYear = (Int16) request.Data.LastYearGrade;
                _context.SalesmenMeta.Update(meta);
            }
            
            // save user
            user.Email = request.Data.Email;
            user.Phone = request.Data.Phone;
            user.IsActive = request.Data.IsActive;
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