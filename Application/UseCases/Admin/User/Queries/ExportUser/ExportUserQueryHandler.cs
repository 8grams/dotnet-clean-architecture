
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
using Microsoft.EntityFrameworkCore;
using CsvHelper;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Misc;

namespace SFIDWebAPI.Application.UseCases.Admin.User.Queries.ExportUser
{
    public class ExportUserQueryHandler : IRequestHandler<ExportUserQuery, ExportUserDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMediator _mediator;
        private readonly IUploader _uploader;
        private readonly IWebHostEnvironment _env;
        private readonly Utils _utils;

        public ExportUserQueryHandler(ISFDDBContext context, IMediator mediator, IUploader uploader, IWebHostEnvironment env, Utils utils)
        {
            _context = context;
            _mediator = mediator;
            _uploader = uploader;
            _env = env;
            _utils = utils;
        }

        public async Task<ExportUserDto> Handle(ExportUserQuery request, CancellationToken cancellationToken)
        {
            var type = "sfd";
            foreach (var filter in request.Filters)
            {
                if (filter.Column == "Type")
                {
                    type = filter.Value;
                }
            }

            if (type.Equals("sfd"))
            {
                return await this._exportSFDUser(request);
            }
            else
            {
                return await this._exportDNETUser(request);
            }
        }

        private async Task<ExportUserDto> _exportDNETUser(ExportUserQuery request)
        {
            var hasActiveFilter = false;
            var isActive = false;
            foreach (var filter in request.Filters)
            {
                if (filter.Column == "IsActive")
                {
                    hasActiveFilter = true;
                    isActive = filter.Value.Equals("true") ? true : false;
                }
            }
            var userIdsQuery = _context.Users.AsQueryable();
            if (hasActiveFilter)
            {
                userIdsQuery = userIdsQuery.Where(e => e.IsActive == isActive);
            }
            var userIds = userIdsQuery.Select(e => e.UserName).ToList();

            var now = DateTime.Now;
            var data = _context.Salesmen
                .Where(e => userIds.Contains(e.SalesmanCode))
                .AsQueryable();

            foreach (var filter in request.Filters)
            {
                if (filter.Column == "DealerCity")
                {
                    var city = _context.Cities.Find(Int16.Parse(filter.Value));
                    data = data.Where(u => u.DealerCity.Equals(city.CityName));
                }

                if (filter.Column == "DealerBranch")
                {
                    var branch = _context.DealerBranches.Find(int.Parse(filter.Value));
                    data = data.Where(u => u.DealerBranchName.Equals(branch.Name));
                }

                if (filter.Column == "DealerName")
                {
                    var dealer = _context.Dealers.Find(int.Parse(filter.Value));
                    data = data.Where(u => u.DealerName.Equals(dealer.DealerName));
                }

                if (filter.Column == "JobPosition")
                {
                    var job = _context.JobPositions.Find(int.Parse(filter.Value));
                    data = data.Where(u => u.JobDescription.Equals(job.Description));
                }
            }

            if (!string.IsNullOrEmpty(request.QuerySearch))
            {
                data = data.Where("SalesmanCode.Contains(@0) || SalesmanName.Contains(@0)", request.QuerySearch);
            }

            var dataQuery = data
                .Select(salesman => 
                    new ExportData() {
                        SalesmanCode = salesman.SalesmanCode,
                        SalesmanName = salesman.SalesmanName,
                        SalesmanEmail = salesman.SalesmanEmail,
                        SalesmanHandphone = salesman.SalesmanHandphone,
                        JobDescription = salesman.JobDescription,
                        DealerCode = salesman.DealerCode,
                        DealerBranchCode = salesman.DealerBranchCode,
                        DealerCity = salesman.DealerCity,
                        DealerName = salesman.DealerName,
                        SalesmanHireDate = salesman.SalesmanHireDate.ToShortDateString(),
                        Grade = "-",
                        SalesmanStatus = salesman.SalesmanStatus
                    }
                );
            
            List<ExportData> csvData = dataQuery.ToList();

            var path = Utils.GetUploadLocation("export", "export_user.csv");
            var location = _env.ContentRootPath + path;
            using(var writer = new StreamWriter(location)) {
                using (var csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
                {
                    csv.WriteRecords(csvData);
                }
                writer.Close();
            }

            var a = await Task.FromResult<ExportUserDto>(null);
            return new ExportUserDto
            {
                Success = true,
                Message = "Export has been successfully executed",
                Data = new ExportUserData
                {
                    Link = _utils.GetValidUrl(path)
                }
            };
        }

        private async Task<ExportUserDto> _exportSFDUser(ExportUserQuery request)
        {
            var hasActiveFilter = false;
            var isActive = false;
            foreach (var filter in request.Filters)
            {
                if (filter.Column == "IsActive")
                {
                    hasActiveFilter = true;
                    isActive = filter.Value.Equals("true") ? true : false;
                }
            }
            var userIdsQuery = _context.Users.AsQueryable();
            if (hasActiveFilter)
            {
                userIdsQuery = userIdsQuery.Where(e => e.IsActive == isActive);
            }
            var userIds = userIdsQuery.Select(e => e.UserName).ToList();
            
            var now = DateTime.Now;
            var data = _context.SalesmenMeta
                .Include(e => e.User)
                .Where(e => userIds.Contains(e.SalesmanCode));

            foreach (var filter in request.Filters)
            {
                if (filter.Column == "DealerCity")
                {
                    var dealerBranches = _context.DealerBranches
                        .Where(e => e.CityId == Int16.Parse(filter.Value))
                        .Select(e => e.Id)
                        .ToList();
                    data = data.Where(u => dealerBranches.Contains(u.DealerBranchId));
                }

                if (filter.Column == "DealerBranch")
                {
                    data = data.Where(u => u.DealerBranchId == Int16.Parse(filter.Value));
                }

                if (filter.Column == "DealerName")
                {
                    var dealerBranches = _context.DealerBranches
                        .Where(e => e.DealerId == Int16.Parse(filter.Value))
                        .Select(e => e.Id)
                        .ToList();
                    data = data.Where(u => dealerBranches.Contains(u.DealerBranchId));
                }

                if (filter.Column == "JobPosition")
                {
                    data = data.Where(u => u.JobPositionId == int.Parse(filter.Value));
                }
            }

            if (!string.IsNullOrEmpty(request.QuerySearch))
            {
                data = data.Where("SalesmanCode.Contains(@0) || SalesmanName.Contains(@0)", request.QuerySearch);
            }
            
            var dataQuery = data.Select(salesman => 
                    new ExportData() {
                        SalesmanCode = salesman.SalesmanCode,
                        SalesmanName = salesman.SalesmanName,
                        SalesmanEmail = salesman.SalesmanEmail,
                        SalesmanHandphone = salesman.SalesmanHandphone,
                        JobDescription = salesman.JobPosition.Description,
                        DealerCode = salesman.DealerBranch.Dealer.DealerCode,
                        DealerBranchCode = salesman.DealerBranch.DealerBranchCode,
                        DealerCity = salesman.DealerBranch.City.CityName,
                        DealerName = salesman.DealerBranch.Dealer.DealerName,
                        SalesmanHireDate = salesman.SalesmanHireDate.ToShortDateString(),
                        Grade = "-",
                        SalesmanStatus = "Aktif"
                    }
                );
            
            List<ExportData> csvData = dataQuery.ToList();

            var path = Utils.GetUploadLocation("export", "export_user.csv");
            var location = _env.ContentRootPath + path;
            using(var writer = new StreamWriter(location)) {
                using (var csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
                {
                    csv.WriteRecords(csvData);
                }
                writer.Close();
            }

            var a = await Task.FromResult<ExportUserDto>(null);
            return new ExportUserDto
            {
                Success = true,
                Message = "Export has been successfully executed",
                Data = new ExportUserData
                {
                    Link = _utils.GetValidUrl(path)
                }
            };
        }
    }
}