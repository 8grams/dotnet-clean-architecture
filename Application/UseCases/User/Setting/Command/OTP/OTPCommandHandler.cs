using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MediatR;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Models.Query;
using AutoMapper;

namespace SFIDWebAPI.Application.UseCases.User.Setting.Command.OTP
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
                .Where(o => o.UserId.Equals(request.UserId))
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

            response.Success = true;
            response.Message = "OTP is valid";
            response.Origin = "otp.success.default";
            return response;
        }
    }
}
