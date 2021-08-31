using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using MediatR;
using AutoMapper;
using StoredProcedureEFCore;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.UseCases.User.PKT.Models;

namespace SFIDWebAPI.Application.UseCases.User.PKT.Report.Command.VerifyVin
{
    public class VerifyVinCommandHandler : IRequestHandler<VerifyVinCommand, VerifyVinDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public VerifyVinCommandHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<VerifyVinDto> Handle(VerifyVinCommand request, CancellationToken cancellationToken)
        {
            List<VinDataSP> spData = null;
            
            _context.loadStoredProcedureBuilder("Sp_GetVinNumber")
                .AddParam("@vinNumber", request.Data.Vin)
                .Exec(r => spData = r.ToList<VinDataSP>());

            var vinData = spData.FirstOrDefault();

            // LOL!
            var a = await Task.FromResult<VinDataSP>(null);
            
            if (vinData == null)
            {
                return new VerifyVinDto()
                {
                    Success = false,
                    Message = "Vin is not found",
                    Origin = "verify_vin.fail.vin_not_found"
                };
            }

            if (request.Data.Type.Equals("vin") && !vinData.SalesmanCode.Equals(request.Data.SalesCode))
            {
                return new VerifyVinDto()
                {
                    Success = false,
                    Message = "Vin is not authorized",
                    Origin = "verify_vin.fail.vin_not_authorized"
                };
            }
            
            return new VerifyVinDto()
            {
                Success = true,
                Message = "Vin is verified",
                Data = _mapper.Map<VerifyVinData>(vinData)
            };
        }
    }
}
