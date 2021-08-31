using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using MediatR;
using CsvHelper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Infrastructure.Persistences;

namespace SFIDWebAPI.Application.UseCases.Admin.User.Command.ImportUser
{
    public class ImportUserCommandHandler : IRequestHandler<ImportUserCommand, ImportUserDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IUploader _uploader;
        private IWebHostEnvironment _env;

        public ImportUserCommandHandler(ISFDDBContext context, IUploader uploader, IWebHostEnvironment env)
        {
            _context = context;
            _uploader = uploader;
            _env = env;
        }

        public async Task<ImportUserDto> Handle(ImportUserCommand request, CancellationToken cancellationToken)
        {
            var fileUrl = await _uploader.UploadFile(request.Data.FileByte, "import", request.Data.FileName);

            using (var reader = new StreamReader(_env.ContentRootPath + fileUrl))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<ImportData>();

                // save to db
                var branches = new Dictionary<string, int?>();
                var jobs = new Dictionary<string, int?>();
                var dealers = new Dictionary<int?, int?>();
                var positionMetaId = _context.PositionMetas.First().Id;
                var salesmanLevelId = _context.SalesmanLevels.First().Id;

                foreach (var item in records)
                {
                    var branchCode = item.DealerBranchCode;
                    var branchId = 0;
                    if (!branches.ContainsKey(branchCode))
                    {
                        var existingBranch = await _context.DealerBranches
                            .Where(e => e.DealerBranchCode.Equals(branchCode))
                            .FirstOrDefaultAsync();
                        if (existingBranch == null) continue;
                        branchId = existingBranch.Id;
                        branches.Add(branchCode, branchId);
                        dealers.Add(branchId, existingBranch.Dealer.Id);
                    }
                    else
                    {
                        branchId = (int) branches[branchCode];
                    }
                    
                    var jobDescription = item.JobDescription;
                    var jobId = 0;
                    if (!jobs.ContainsKey(jobDescription))
                    {
                        var existingJob = await _context.JobPositions
                            .Where(e => e.Description.Equals(jobDescription))
                            .FirstOrDefaultAsync();

                        if (existingJob == null) continue;
                        jobId = existingJob.Id;
                        jobs.Add(jobDescription, jobId);
                    }
                    else
                    {
                        jobId = (int) jobs[jobDescription];
                    }

                    var salesmanMeta = new Domain.Entities.SalesmanMeta 
                    {
                        SalesmanCode = item.SalesmanCode,
                        SalesmanName = item.SalesmanName,
                        SalesmanEmail = item.SalesmanEmail,
                        SalesmanHandphone = "0000",
                        SuperiorName = "Admin",
                        SalesmanHireDate = DateTime.Now,
                        JobPositionId = (int) jobId,
                        SalesmanLevelId = salesmanLevelId,
                        DealerBranchId = (int) branchId,
                        PositionMetaId = positionMetaId,
                        GradeLastYear = 1,
                        GradeCurrentYear = 1
                    };
                    
                    _context.SalesmenMeta.Add(salesmanMeta);
                    await _context.SaveChangesAsync(cancellationToken);

                    // create user
                    _context.Users.Add(new Domain.Entities.User
                    {
                        Email = salesmanMeta.SalesmanEmail,
                        Phone = salesmanMeta.SalesmanHandphone,
                        UserName = salesmanMeta.SalesmanCode,
                        Password = DBUtil.PasswordHash(item.DealerCode),
                        IsActive = true,
                        DeviceId = "000",
                        LastLogin = DateTime.Now,
                        MasterConfigId = 1,
                        LoginThrottle = 0,
                        DealerId = dealers[branchId]
                    });
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }

            return new ImportUserDto
            {
                Success = true,
                Message = "Import user is successfully executed"
            };
        }
    }
}