using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MediatR;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Infrastructure.Persistences;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Domain.Entities;
using SFIDWebAPI.Application.Misc;

namespace SFIDWebAPI.Application.UseCases.User.Auth.Command.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public LoginCommandHandler(ISFDDBContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<LoginDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            // delete inactive user
            var inactiveUsers = await _context.Users
                .Where(e => e.UserName.Equals(request.Data.SalesCode))
                .Where(e => e.IsActive == false)
                .ToListAsync();
            _context.Users.RemoveRange(inactiveUsers);
            await _context.SaveChangesAsync(cancellationToken);

            var user = await _context.Users
                .Where(u => u.UserName.Equals(request.Data.SalesCode))
                .Where(u => u.IsActive == true)
                .Include(u => u.SalesmanData)
                .Include(u => u.AccessTokens)
                .FirstOrDefaultAsync();

            var response = new LoginDto();
            if (user != null)
            {
                if (user.Password.Equals(DBUtil.PasswordHash(request.Data.Password)))
                {
                    // set is registered
                    user.IsRegistered = true;

                    response.Success = true;
                    response.Message = "Login success, user terdaftar";
                    response.Origin = "login.success.default";
                    var profileData = _mapper.Map<ProfileDto>(user);

                    // save current device id
                    user.DeviceId = request.Data.DeviceId;
                    user.LastLogin = DateTime.Now;
                    user.LoginThrottle = 0;
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync(cancellationToken);

                    // generate token
                    var token = DBUtil.GenerateAuthToken();
                    var expiresAt = DateTime.Now.AddDays(1);
                    var tokenData = new AccessToken()
                    {
                        AuthToken = token,
                        Type = "Bearer",
                        UserId = user.Id,
                        ExpiresAt = expiresAt
                    };
                    _context.AccessTokens.Add(tokenData);
                    await _context.SaveChangesAsync(cancellationToken);

                    profileData.AccessToken = new OTPData()
                    {
                        AuthToken = token,
                        Type = "Bearer",
                        ExpiresAt = Utils.DateToTimestamps(expiresAt)
                    };
                    response.Data = profileData;
                }
                else
                {
                    // adding login throttle
                    user.LoginThrottle = user.LoginThrottle + 1;
                    if (user.LoginThrottle >= 3)
                    {
                        // send password to email
                        // execute after event
                        await _mediator.Publish(new SendPassword
                        {
                            User = user
                        }, cancellationToken);

                        // reset throttle
                        user.LoginThrottle = 0;
                    }
                    await _context.SaveChangesAsync(cancellationToken);

                    response.Success = false;
                    response.Message = "Password salah";
                    response.Origin = "login.fail.wrong_password";
                }          
            }
            // if not exists, check on salesmen data
            else 
            {
                var sales = await _context.Salesmen
                    .Include(e => e.User)
                    .Where(s => s.SalesmanCode.Equals(request.Data.SalesCode))
                    .FirstOrDefaultAsync();

                if (sales == null)
                {
                    response.Success = false;
                    response.Message = "Login Failed";
                    response.Origin = "login.fail.salesman_not_registered";
                }
                else
                {
                    if (sales.DealerCode.Equals(request.Data.Password))
                    {
                        response.Success = true;
                        response.Message = "Login success, user belum terdaftar";
                        response.Origin = "login.success.user_not_registered";
                        response.Data = _mapper.Map<ProfileDto>(sales);
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Password Salah";
                        response.Origin = "login.fail.salesman_wrong_password";
                    }  
                }
            }

            return response;
        }
    }
}
