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

namespace SFIDWebAPI.Application.UseCases.Admin.Report.Queries.ExportTrainingMaterial
{
    public class ExportTrainingMaterialQueryHandler : IRequestHandler<ExportTrainingMaterialQuery, ExportTrainingMaterialDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMediator _mediator;
        private readonly IUploader _uploader;
        private readonly IWebHostEnvironment _env;
        private readonly Utils _utils;

        public ExportTrainingMaterialQueryHandler(ISFDDBContext context, IMediator mediator, IUploader uploader, IWebHostEnvironment env, Utils utils)
        {
            _context = context;
            _mediator = mediator;
            _uploader = uploader;
            _env = env;
            _utils = utils;
        }

        public async Task<ExportTrainingMaterialDto> Handle(ExportTrainingMaterialQuery request, CancellationToken cancellationToken)
        {
            var dataQueryQ =  _context.MaterialCounters
                .Where(e => e.CreateDate >= request.StartDate)
                .Where(e => e.CreateDate <= request.EndDate);

            var userTrainings = _context.MaterialCounters
                .Where(e => e.ContentType.Equals("training"))
                .Select(e => e.UserId)
                .Distinct()
                .ToList();

            var users = _context.Users
                .Where(e => userTrainings.Contains(e.Id))
                .Select(e => e.UserName)
                .ToList();

            var hasCategoryFilter = false;
            var categoryFilter = 0;

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

                if (filter.Column.Equals("MasterCarId")) 
                {
                    hasCategoryFilter = true;
                    categoryFilter = int.Parse(filter.Value);
                }
            }

            var dataQueryR =  dataQueryQ
                .GroupBy(e => new {
                    ContentId = e.ContentId,
                    ContentType = e.ContentType
                })
                .Select(e => new {
                    ContentId = e.Key.ContentId,
                    ContentType = e.Key.ContentType,
                    TotalViews = e.Count()
                })
                .Where(e => e.ContentType.Equals("training"))
                .Join(
                    _context.TrainingMaterials,
                    p => p.ContentId,
                    q => q.Id,
                    (p, q) => new{
                        Id = q.Id,
                        FileCode = q.FileCode,
                        Content = p.ContentType,
                        Title = q.Title,
                        MaterialType = q.FileType.Equals("mp4") ? "Video" : "Bacaan",
                        TotalView = q.TotalViews,
                        MasterCarId = q.MasterCarId,
                        TotalUserVisited = 0
                    }
                )
                .Join(
                    _context.MasterCars,
                    r => r.MasterCarId,
                    s => s.Id,
                    (r, s) => new{
                        Id = r.Id,
                        FileCode = r.FileCode,
                        Content = r.Content,
                        Title = r.Title,
                        MasterCarId = s.Id,
                        Category = s.Title,
                        CarType = s.Tag,
                        MaterialType = r.MaterialType,
                        TotalView = r.TotalView,
                        TotalUserVisited = r.TotalUserVisited
                    }
                );
            
            if (hasCategoryFilter)
            {
                dataQueryR = dataQueryR.Where(e => e.MasterCarId == categoryFilter); 
            }

            var dataQuery = dataQueryR.Select(e => new ExportData {
                    Id = e.Id,
                    FileCode = e.FileCode,
                    Content = e.Content,
                    Title = e.Title,
                    Category = e.Category,
                    CarType = e.CarType,
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

            var path = Utils.GetUploadLocation("export", "export_training.csv");
            var location = _env.ContentRootPath + path;
            using(var writer = new StreamWriter(location)) {
                using (var csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
                {
                    csv.Configuration.RegisterClassMap<ExportMap>();
                    csv.WriteRecords(csvData);
                }
                writer.Close();
            }

            var a = await Task.FromResult<ExportTrainingMaterialDto>(null);
            return new ExportTrainingMaterialDto
            {
                Success = true,
                Message = "Export bulletin has been successfully executed",
                Data = new ExportTrainingMaterialData
                {
                    Link = _utils.GetValidUrl(path)
                }
            };
        }
        
        private int getTotalUserVisited(int contentId, List<int> users)
        {
            return _context.MaterialCounters
                .Where(e => e.ContentType.Equals("training"))
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