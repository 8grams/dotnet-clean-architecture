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

namespace SFIDWebAPI.Application.UseCases.Admin.Report.Queries.Bulletin
{
    public class GetBulletinReportQueryHandler : IRequestHandler<GetBulletinReportQuery, GetBulletinReportDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public GetBulletinReportQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetBulletinReportDto> Handle(GetBulletinReportQuery request, CancellationToken cancellationToken)
        {
            // top menu //
            // active account
            var totalBulletin =  await _context.Bulletins
                .CountAsync();
            
            var totalBulletinVisited = await _context.MaterialCounters
                .Where(e => e.ContentType.Equals("bulletin"))
                .Select(e => new { e.ContentType, e.ContentId })
                .CountAsync();

            var topMenu = new TopMenuData {
                Items = new List<TopMenuDataItem> {
                    new TopMenuDataItem {
                        Id = 1,
                        Name = "Total Salesman Bulletin",
                        Key = "total_bulletin",
                        Value = totalBulletin.ToString(),
                        Color = "#CA0176"
                    },
                    new TopMenuDataItem {
                        Id = 2,
                        Name = "Total Salesman Bulletin Visited",
                        Key = "total_bulletin_visited",
                        Value = totalBulletinVisited.ToString(),
                        Color = "#CA5501"
                    }
                }
            };

            var thisWeekAccessQuery = _context.MaterialCounters
                .Where(e => e.ContentType.Equals("bulletin"))
                .Where(e => e.CreateDate >= request.StartDate)
                .Where(e => e.CreateDate <= request.EndDate);

            var totalVisitQuery = _context.MaterialCounters
                .Where(e => e.ContentType.Equals("bulletin"))
                .Where(e => e.CreateDate >= request.StartDate)
                .Where(e => e.CreateDate <= request.EndDate).AsQueryable();

            var usersBulletins = _context.MaterialCounters
                .Where(e => e.ContentType.Equals("bulletin"))
                .Select(e => e.UserId)
                .Distinct()
                .ToList();

            var users = _context.Users
                .Where(e => usersBulletins.Contains(e.Id))
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
                var bulletin = _context.Bulletins.Find(e.ContentId);
                if (bulletin != null) {
                    topVisits.Add(new TopVisitItem {
                        Id = bulletin.Id,
                        Title = bulletin.Title,
                        TotalUser = e.TotalUserVisit,
                        TotalVisit = bulletin.TotalViews
                    });
                    counter++;
                }
            });

            topVisits = topVisits.OrderByDescending(e => e.TotalVisit).ThenByDescending(e => e.TotalUser).ToList();
            
            return new GetBulletinReportDto()
            {
                Success = true,
                Message = "BulletinReport data are succefully retrieved",
                Data = new BulletinReportData {
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
