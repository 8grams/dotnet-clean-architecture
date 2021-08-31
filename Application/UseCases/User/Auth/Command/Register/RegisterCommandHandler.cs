using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AutoMapper;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Infrastructure.Persistences;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.UseCases.User.Auth.Event;

namespace SFIDWebAPI.Application.UseCases.User.Auth.Command.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public RegisterCommandHandler(ISFDDBContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<RegisterDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // delete inactive user
            var inactiveUsers = await _context.Users
                .Where(e => e.UserName.Equals(request.Data.SalesCode))
                .Where(e => e.IsActive == false || e.RowStatus == -1)
                .ToListAsync();

            _context.Users.RemoveRange(inactiveUsers);
            await _context.SaveChangesAsync(cancellationToken);
            
            // create dto
            var response = new RegisterDto();

            // get salesman data first
            var salesman = await _context.Salesmen
                .Where(a => a.SalesmanCode.Equals(request.Data.SalesCode))
                .FirstOrDefaultAsync();

            if (salesman == null)
            {
                response.Success = false;
                response.Message = "Data tidak ditemukan";
                response.Origin = "register.fail.salesman_not_found";
                return response;
            }

            var existingUser = await _context.Users
                .Where(e => e.UserName.Equals(request.Data.SalesCode))
                .FirstOrDefaultAsync();
                
            if (existingUser == null)
            {
                // get dealer id
                var dealer = await _context.Dealers.Where(e => e.DealerName.Equals(salesman.DealerName)).FirstOrDefaultAsync();

                // salesman found
                var user = new Domain.Entities.User()
                {
                    Email = request.Data.Email,
                    Phone = request.Data.Phone,
                    UserName = request.Data.SalesCode,
                    Password = DBUtil.PasswordHash(request.Data.Password),
                    IsActive = false,
                    DeviceId = request.Data.DeviceId,
                    LastLogin = DateTime.Now,
                    MasterConfigId = 1,
                    LoginThrottle = 0,
                    DealerId = dealer.Id
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync(cancellationToken);
                existingUser = await _context.Users
                    .Include(e => e.SalesmanData)
                    .Where(e => e.UserName.Equals(request.Data.SalesCode)).FirstAsync();
            }

            existingUser.IsRegistered = true;

            // execute after event
            await _mediator.Publish(new SendOTP
            {
                User = existingUser
            }, cancellationToken);

            // get newest user
            response.Success = true;
            response.Message = "Register Success";
            response.Origin = "register.success.default";
            response.Data = _mapper.Map<ProfileDto>(existingUser);

            return response;
        }
    }

}
