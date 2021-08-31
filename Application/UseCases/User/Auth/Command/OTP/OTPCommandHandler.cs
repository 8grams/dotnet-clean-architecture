using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MediatR;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Infrastructure.Persistences;
using SFIDWebAPI.Domain.Entities;
using SFIDWebAPI.Application.Models.Query;
using AutoMapper;

namespace SFIDWebAPI.Application.UseCases.User.Auth.Command.OTP
{
    public class OTPCommandHandler : IRequestHandler<OTPCommand, OTPDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public OTPCommandHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OTPDto> Handle(OTPCommand request, CancellationToken cancellationToken)
        {
            var otp = await _context.OTPs
                .Where(o => o.Pin.Equals(request.Data.Pin))
                .Where(o => o.UserId.Equals(request.Data.UserId))
                .OrderByDescending(e => e.CreateDate)
                .FirstOrDefaultAsync();

            var response = new OTPDto();
            if (otp == null)
            {
                response.Success = false;
                response.Message = "Kode OTP salah, silakan cek kembali";
                response.Origin = "otp.fail.invalid_otp";
                return response;
            }

            if (otp.ExpiresAt < DateTime.Now)
            {
                response.Success = false;
                response.Message = "Kode OTP sudah expired, silakan lakukan request ulang.";
                response.Origin = "otp.fail.expired_otp";
                return response;
            }

            // update user
            var updateUser = await _context.Users.FindAsync(request.Data.UserId);
            updateUser.IsActive = true;

            // generate token
            var accessToken = DBUtil.GenerateAuthToken();
            var expiresAt = DateTime.Now.AddDays(1);
            var tokenData = new AccessToken()
            {
                AuthToken = accessToken,
                Type = "Bearer",
                UserId = request.Data.UserId,
                ExpiresAt = expiresAt
            };

            _context.AccessTokens.Add(tokenData);

            await _context.SaveChangesAsync(cancellationToken);

            response.Success = true;
            response.Message = "OTP is Valid";
            response.Origin = "otp.success.default";
            response.Data = _mapper.Map<OTPData>(tokenData);

            return response;
        }
    }
}
