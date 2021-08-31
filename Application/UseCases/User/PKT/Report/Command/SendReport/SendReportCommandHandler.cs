using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.UseCases.User.PKT.Models;
using StoredProcedureEFCore;

namespace SFIDWebAPI.Application.UseCases.User.PKT.Report.Command.SendReport
{
    public class SendReportCommandHandler : IRequestHandler<SendReportCommand, SendReportDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;
        private readonly IUploader _uploader;

        public SendReportCommandHandler(ISFDDBContext context, IMapper mapper, IUploader uploader)
        {
            _context = context;
            _mapper = mapper;
            _uploader = uploader;
        }

        public async Task<SendReportDto> Handle(SendReportCommand request, CancellationToken cancellationToken)
        {
            var status = 1;
            if (request.Data.Status != null)
            {
                status = (int) request.Data.Status;
            }

            // check vin is valid
            List<VinDataSP> spData = null;
            
            _context.loadStoredProcedureBuilder("Sp_GetVinNumber")
                .AddParam("vinNumber", request.Data.Vin)
                .Exec(r => spData = r.ToList<VinDataSP>());

            var vinData = spData.FirstOrDefault();

            if (vinData == null && status != 0)
            {
                return new SendReportDto()
                {
                    Success = false,
                    Message = "Vin is not found",
                    Origin = "pkt_send_report.fail.vin_not_found"
                };
            }

            if (!vinData.SalesmanCode.Equals(request.Data.SalesCode) && status != 0)
            {
                return new SendReportDto()
                {
                    Success = false,
                    Message = "Vin is not authorized",
                    Origin = "verify_vin.fail.vin_not_authorized"
                };
            }

            var pdiDate = vinData.PDIDate;
            if ( (pdiDate == null || (DateTime.Now - (DateTime)pdiDate ).TotalDays > 30) && status != 0 )
            {
                return new SendReportDto()
                {
                    Success = false,
                    Message = "PDI Date not valid",
                    Origin = "pkt_send_report.fail.invalid_pdi_date"
                };
            }

            // check if exists
            var exists = await _context.PKTHistories
                .Where(e => e.Vin.Equals(request.Data.Vin))
                .FirstOrDefaultAsync();

            if (exists != null && status != 0) {
                return new SendReportDto()
                {
                    Success = false,
                    Message = "Report is already sent",
                    Origin = "pkt_send_report.fail.report_already_sent"
                };
            }

            // upload file
            string fileUrl = null;
            if (!string.IsNullOrEmpty(request.Data.Image))
            {
                fileUrl = await _uploader.UploadFile(request.Data.Image, "vin", "vin_" + vinData.ChassisNumber.ToString() + ".jpg");
            }

            // delete old draft with same vin
            var histories = await _context.PKTHistories
                .Where(e => e.Vin.Equals(request.Data.Vin))
                .Where(e => e.Status == 0)
                .ToListAsync();
            _context.PKTHistories.RemoveRange(histories);
            await _context.SaveChangesAsync(cancellationToken);

            _context.PKTHistories.Add(new Domain.Entities.PKTHistory
            {
                Vin = request.Data.Vin,
                Image = fileUrl,
                SalesCode = request.Data.SalesCode,
                PDIDate = (DateTime) vinData.PDIDate,
                Status = status,
                ReportType = request.Data.Type
            });

            await _context.SaveChangesAsync(cancellationToken);
            
            return new SendReportDto()
            {
                Success = true,
                Message = "Report is successfully sent"
            };
        }
    }
}
