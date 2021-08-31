using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Infrastructure.Persistences;

namespace SFIDWebAPI.Application.UseCases.Admin.User.Command.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMediator _mediator;
        private readonly IUploader _uploader;

        public CreateUserCommandHandler(ISFDDBContext context, IMediator mediator, IUploader uploader)
        {
            _context = context;
            _mediator = mediator;
            _uploader = uploader;
        }

        public async Task<CreateUserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var jobPosition = await _context.JobPositions.FindAsync(request.Data.JobPositionId);
            var salesmanLevel = await _context.SalesmanLevels.FindAsync(request.Data.LevelId);
            var dealerBranch = await _context.DealerBranches
                .Include(e => e.Dealer)
                .Include(e => e.City)
                .Include(e => e.Dealer.DealerGroup)
                .Where(e => e.Id == request.Data.DealerBranchId)
                .FirstAsync();

            // save meta
            var meta = new Domain.Entities.SalesmanMeta
            {
                SalesmanCode = request.Data.SalesmanCode,
                SalesmanName = request.Data.SalesmanName,
                SalesmanHireDate = request.Data.JoinDate,
                SuperiorName = request.Data.SupervisorName,
                SalesmanEmail = request.Data.Email,
                SalesmanHandphone = request.Data.Phone,
                DealerBranchId = request.Data.DealerBranchId,
                SalesmanLevelId = request.Data.LevelId,
                GradeCurrentYear = (Int16) request.Data.CurrentYearGrade,
                GradeLastYear = (Int16) request.Data.LastYearGrade,
            };

            if (!string.IsNullOrEmpty(request.Data.JobPositionId.ToString()))
            {
                meta.JobPositionId = request.Data.JobPositionId;
            }

            if (!string.IsNullOrEmpty(request.Data.PositionMetaId.ToString()))
            {
                meta.PositionMetaId = request.Data.PositionMetaId;
            }
            _context.SalesmenMeta.Add(meta);

            // save user
            var user = new Domain.Entities.User
            {
                Email = request.Data.Email,
                Phone = request.Data.Phone,
                UserName = request.Data.SalesmanCode,
                Password = DBUtil.PasswordHash(request.Data.Password),
                IsActive = true,
                DeviceId = DBUtil.PasswordHash(DateTime.Now.ToString()).Substring(0,5),
                LastLogin = null,
                MasterConfigId = 1,
                LoginThrottle = 0
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