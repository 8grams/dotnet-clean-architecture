using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.IO;
using System.Globalization;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using CsvHelper;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Misc;

namespace SFIDWebAPI.Application.UseCases.Admin.Report.Queries.ExportInfo
{
    public class ExportInfoQueryHandler : IRequestHandler<ExportInfoQuery, ExportInfoDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMediator _mediator;
        private readonly IUploader _uploader;
        private readonly IWebHostEnvironment _env;
        private readonly Utils _utils;

        public ExportInfoQueryHandler(ISFDDBContext context, IMediator mediator, IUploader uploader, IWebHostEnvironment env, Utils utils)
        {
            _context = context;
            _mediator = mediator;
            _uploader = uploader;
            _env = env;
            _utils = utils;
        }

        public async Task<ExportInfoDto> Handle(ExportInfoQuery request, CancellationToken cancellationToken)
        {
            var dataQueryQ =  _context.MaterialCounters
                .Where(e => e.CreateDate >= request.StartDate)
                .Where(e => e.CreateDate <= request.EndDate);

            var userInfos = _context.MaterialCounters
                .Where(e => e.ContentType.Equals("info"))
                .Select(e => e.UserId)
                .Distinct()
                .ToList();

            var users = _context.Users
                .Where(e => userInfos.Contains(e.Id))
                .Select(e => e.UserName)
                .ToList();

            foreach (var filter in request.Filters)
            {
                if (filter.Column.Equals("DealerId")) 
                {
                    var dealerCode = _context.Dealers.Find(int.Parse(filter.Value)).DealerCode;
                    var salesCodes = _context.Salesmen
                        .Where(e => users.Contains(e.SalesmanCode))
                        .Where(e => e.DealerCode.Equals(dealerCode))
                        .Select(e => e.SalesmanCode)
                        .ToList();
                    
                    var usersInvolved = _context.Users
                        .Where(e => salesCodes.Contains(e.UserName))
                        .Select(e => e.Id)
                        .ToList();

                    dataQueryQ = dataQueryQ
                        .Where(e => usersInvolved.Contains(e.UserId));
                }

                if (filter.Column.Equals("CityId")) 
                {
                    var cityName = _context.Cities.Find(Int16.Parse(filter.Value)).CityName;
                    var salesCodes = _context.Salesmen
                        .Where(e => users.Contains(e.SalesmanCode))
                        .Where(e => e.DealerCity.Equals(cityName))
                        .Select(e => e.SalesmanCode)
                        .ToList();
                        
                    var usersInvolved = _context.Users
                        .Where(e => salesCodes.Contains(e.UserName))
                        .Select(e => e.Id)
                        .ToList();

                    dataQueryQ = dataQueryQ
                        .Where(e => usersInvolved.Contains(e.UserId));
                }

                if (filter.Column.Equals("DealerBranchId")) 
                {
                    var branchCode = _context.DealerBranches.Find(int.Parse(filter.Value)).DealerBranchCode;
                    var salesCodes = _context.Salesmen
                        .Where(e => users.Contains(e.SalesmanCode))
                        .Where(e => e.DealerBranchCode.Equals(branchCode))
                        .Select(e => e.SalesmanCode)
                        .ToList();
                    
                    var usersInvolved = _context.Users
                        .Where(e => salesCodes.Contains(e.UserName))
                        .Select(e => e.Id)
                        .ToList();

                    dataQueryQ = dataQueryQ
                        .Where(e => usersInvolved.Contains(e.UserId));
                }
            }

            var dataQuery =  dataQueryQ.GroupBy(e => new {
                    ContentId = e.ContentId,
                    ContentType = e.ContentType
                })
                .Select(e => new {
                    ContentId = e.Key.ContentId,
                    ContentType = e.Key.ContentType,
                    TotalViews = e.Count()
                })
                .Where(e => e.ContentType.Equals("info"))
                .Join(
                    _context.AdditionalInfos,
                    p => p.ContentId,
                    q => q.Id,
                    (p, q) => new{
                        Id = q.Id,
                        FileCode = q.FileCode,
                        Content = p.ContentType,
                        Title = q.Title,
                        MaterialType = q.FileType.Equals("mp4") ? "Video" : "Bacaan",
                        TotalView = q.TotalViews,
                        TotalUserVisited = 0
                    }
                )
                .Select(e => new ExportData {
                    Id = e.Id,
                    FileCode = e.FileCode,
                    Content = e.Content,
                    Title = e.Title,
                    MaterialType = e.MaterialType,
                    TotalView = e.TotalView,
                    TotalUserVisited = e.TotalUserVisited
                });

            List<ExportData> csvDataQ = dataQuery.ToList();
            List<ExportData> csvData = new List<ExportData>();
            var counter = 1;
            var usersUsed = dataQueryQ.Select(e => e.UserId).ToList();
            csvDataQ.ForEach(e => {
                e.No = counter++;
                e.TotalUserVisited = this.getTotalUserVisited((int)e.Id, usersUsed);
                csvData.Add(e);
            });

            var path = Utils.GetUploadLocation("export", "export_info.csv");
            var location = _env.ContentRootPath + path;
            using(var writer = new StreamWriter(location)) {
                using (var csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
                {
                    csv.Configuration.RegisterClassMap<ExportMap>();
                    csv.WriteRecords(csvData);
                }
                writer.Close();
            }

            var a = await Task.FromResult<ExportInfoDto>(null);
            return new ExportInfoDto
            {
                Success = true,
                Message = "Export Additional Info has been successfully executed",
                Data = new ExportInfoData
                {
                    Link = _utils.GetValidUrl(path)
                }
            };
        }
        
        private int getTotalUserVisited(int contentId, List<int> users)
        {
            return _context.MaterialCounters
                .Where(e => e.ContentType.Equals("info"))
                .Where(e => e.ContentId == contentId)
                .Where(e => users.Contains(e.UserId))
                .Select(e => new {
                    ContentId = e.ContentId,
                    ContentType = e.ContentType,
                    UserId = e.UserId
                })
                .Distinct()
                .Count();
        }
    }
}