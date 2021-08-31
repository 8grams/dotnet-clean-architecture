using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Dynamic.Core;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;

namespace SFIDWebAPI.Application.UseCases.Admin.Report.Queries.Info
{
    public class GetInfoReportQueryHandler : IRequestHandler<GetInfoReportQuery, GetInfoReportDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public GetInfoReportQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetInfoReportDto> Handle(GetInfoReportQuery request, CancellationToken cancellationToken)
        {
            // top menu //
            // active account
            var totalInfo =  await _context.AdditionalInfos
                .CountAsync();
            
            var totalInfoVisited = await _context.MaterialCounters
                .Where(e => e.ContentType.Equals("info"))
                .Select(e => new { e.ContentType, e.ContentId })
                .CountAsync();

            var topMenu = new TopMenuData {
                Items = new List<TopMenuDataItem> {
                    new TopMenuDataItem {
                        Id = 1,
                        Name = "Total Additional Information",
                        Key = "total_info",
                        Value = totalInfo.ToString(),
                        Color = "#CA0176"
                    },
                    new TopMenuDataItem {
                        Id = 2,
                        Name = "Total Additional Information Visited",
                        Key = "total_info_visited",
                        Value = totalInfoVisited.ToString(),
                        Color = "#CA5501"
                    }
                }
            };

            var thisWeekAccessQuery = _context.MaterialCounters
                .Where(e => e.ContentType.Equals("info"))
                .Where(e => e.CreateDate >= request.StartDate)
                .Where(e => e.CreateDate <= request.EndDate);

            var totalVisitQuery = _context.MaterialCounters
                .Where(e => e.ContentType.Equals("info"))
                .Where(e => e.CreateDate >= request.StartDate)
                .Where(e => e.CreateDate <= request.EndDate).AsQueryable();

            var usersInfos = _context.MaterialCounters
                .Where(e => e.ContentType.Equals("info"))
                .Select(e => e.UserId)
                .Distinct()
                .ToList();

            var users = _context.Users
                .Where(e => usersInfos.Contains(e.Id))
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

                    thisWeekAccessQuery = thisWeekAccessQuery
                        .Where(e => usersInvolved.Contains(e.UserId));

                    totalVisitQuery = totalVisitQuery
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

                    thisWeekAccessQuery = thisWeekAccessQuery
                        .Where(e => usersInvolved.Contains(e.UserId));

                    totalVisitQuery = totalVisitQuery
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

                    thisWeekAccessQuery = thisWeekAccessQuery
                        .Where(e => usersInvolved.Contains(e.UserId));

                    totalVisitQuery = totalVisitQuery
                        .Where(e => usersInvolved.Contains(e.UserId));
                }
            }

            var thisWeekAccessData = thisWeekAccessQuery
                .GroupBy(e => new {
                    Day = e.CreateDate.Date
                })
                .Select(u => new {
                    Period = u.Key.Day,
                    Total = u.Count()
                })
                .OrderByDescending(e => e.Total)
                .ToList();


            var thisWeekAccess = new List<ChartDataItem>();
            thisWeekAccessData.ForEach(e => {
                thisWeekAccess.Add(new ChartDataItem {
                    Period = e.Period.ToString(),
                    Total = e.Total
                });
            });
            thisWeekAccess = thisWeekAccess.OrderBy(e => e.Period).ToList();

            var totalVisitData = totalVisitQuery
                .Select(e => new {
                    ContentId = e.ContentId,
                    ContentType = e.ContentType,
                    UserId = e.UserId
                })
                .Distinct()
                .GroupBy(e => e.ContentId)
                .Select(u => new {
                    ContentId = u.Key,
                    TotalUserVisit = u.Count()
                })
                .OrderByDescending(e => e.TotalUserVisit)
                .ToList();

            var topVisits = new List<TopVisitItem>();
            var counter = 0;
            totalVisitData.ForEach(e => {
                if (counter >= 3) return;
                var info = _context.AdditionalInfos.Find(e.ContentId);
                if (info != null) {
                    topVisits.Add(new TopVisitItem {
                        Id = info.Id,
                        Title = info.Title,
                        TotalUser = e.TotalUserVisit,
                        TotalVisit = info.TotalViews
                    });

                    counter++;
                }
            });

            topVisits = topVisits.OrderByDescending(e => e.TotalVisit).ThenByDescending(e => e.TotalUser).ToList();
            
            return new GetInfoReportDto()
            {
                Success = true,
                Message = "Info Report data are succefully retrieved",
                Data = new InfoReportData {
                    TopMenu = topMenu,
                    Charts = new ChartData {
                        Items = thisWeekAccess
                    },
                    TopVisit = new TopVisit {
                        Items = topVisits
                    }
                }
            };
        }
    }
}
