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

namespace SFIDWebAPI.Application.UseCases.Admin.Report.Queries.Users
{
    public class GetUsersReportQueryHandler : IRequestHandler<GetUsersReportQuery, GetUsersReportDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public GetUsersReportQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetUsersReportDto> Handle(GetUsersReportQuery request, CancellationToken cancellationToken)
        {
            // active account
            // Total account
            var totalAccount = await _context.Salesmen.CountAsync();
            var totalUsers = await _context.Users.CountAsync();
            var activeUsers = await _context.Users
                .Where(e => e.LastLogin > DateTime.Now.AddDays(-1))
                .CountAsync();

            var inactiveUsers = totalUsers - activeUsers;

            // average access user
            var perWeekAccess = _context.UserPresences
                .Where(e => e.IsNew)
                .GroupBy(e => new {
                    Year = e.CreateDate.Year,
                    Week = 1 + (e.CreateDate.DayOfYear)/7
                })
                .Select(u => new {
                    Week = u.Key,
                    Total = u.Count()
                })
                .OrderByDescending(e => e.Total)
                .ToList();

            // most access user per week
            var highestAccessInWeek = perWeekAccess.FirstOrDefault().Total;

            var averageAccessPerDay = _context.UserPresences
                .Where(e => e.IsNew)
                .GroupBy(e => new {
                    Day = e.CreateDate.Day
                })
                .Select(u => new {
                    Day = u.Key,
                    Total = u.Count()
                })
                .Average(u => u.Total);
            averageAccessPerDay = Math.Floor(averageAccessPerDay);

            var topMenu = new TopMenuData {
                Items = new List<TopMenuDataItem> {
                    new TopMenuDataItem {
                        Id = 1,
                        Name = "Active Accounts",
                        Key = "active_accounts",
                        Value = totalUsers.ToString() + "/" + totalAccount.ToString(),
                        Color = "#01CAB9"
                    },
                    new TopMenuDataItem {
                        Id = 2,
                        Name = "Active users",
                        Key = "active_users",
                        Value = activeUsers.ToString(),
                        Color = "#CA5501"
                    },
                    new TopMenuDataItem {
                        Id = 3,
                        Name = "Inactive users",
                        Key = "inactive_users",
                        Value = inactiveUsers.ToString(),
                        Color = "#CA0176"
                    },

                    // most access
                    new TopMenuDataItem {
                        Id = 4,
                        Name = "Most Access Users",
                        Key = "most_access_users",
                        Value = highestAccessInWeek.ToString(),
                        Color = "#CAB901"
                    }, 
                    // average access
                    new TopMenuDataItem {
                        Id = 5,
                        Name = "Average Access Users",
                        Key = "average_access_users",
                        Value = averageAccessPerDay.ToString(),
                        Color = "#12CA01"
                    }, 
                }
            };

            // chart data
            var thisWeekAccessQuery = _context.UserPresences
                .Where(e => e.IsNew)
                .Where(e => e.CreateDate >= request.StartDate)
                .Where(e => e.CreateDate <= request.EndDate);

            // user active
            var userAccessesQuery = _context.UserPresences
                .Where(e => e.IsNew)
                .Where(e => e.CreateDate >= request.StartDate)
                .Where(e => e.CreateDate <= request.EndDate);

            // pkt
            var pktSubmittedQuery = _context.PKTHistories
                .Where(e => e.CreateDate >= request.StartDate)
                .Where(e => e.CreateDate <= request.EndDate);

            var users = _context.Users.Select(e => e.UserName).ToList();
            Domain.Entities.User selectedUser = null;
            
            foreach (var filter in request.Filters)
            {
                if (filter.Column.Equals("SalesCode")) 
                {
                    var user = _context.Users
                        .Where(e => e.UserName.Equals(filter.Value))
                        .FirstOrDefault();
                    
                    if (user != null)
                    {
                        thisWeekAccessQuery = thisWeekAccessQuery
                            .Where(e => e.UserId == user.Id);

                        userAccessesQuery = userAccessesQuery
                            .Where(e => e.UserId == user.Id);

                        pktSubmittedQuery = pktSubmittedQuery
                            .Where(e => e.SalesCode.Equals(user.UserName));
                    }
                    else
                    {
                        thisWeekAccessQuery = thisWeekAccessQuery
                            .Where(e => 1 != 1);

                        userAccessesQuery = userAccessesQuery
                            .Where(e => 1 != 1);

                        pktSubmittedQuery = pktSubmittedQuery
                            .Where(e => 1 != 1);
                    }

                    selectedUser = user;
                }

                if (filter.Column.Equals("JobPositionId")) 
                {
                    thisWeekAccessQuery = thisWeekAccessQuery
                        .Where(e => e.JobPositionId == int.Parse(filter.Value));

                    userAccessesQuery = userAccessesQuery
                        .Where(e => e.JobPositionId == int.Parse(filter.Value));

                    // get job position
                    var jobPositionDescription = _context.JobPositions.Find(int.Parse(filter.Value)).Description;
                    var salesCodes = _context.Salesmen
                        .Where(e => users.Contains(e.SalesmanCode))
                        .Where(e => e.JobDescription.Equals(jobPositionDescription))
                        .Select(e => e.SalesmanCode)
                        .ToList();

                    pktSubmittedQuery = pktSubmittedQuery
                        .Where(e => salesCodes.Contains(e.SalesCode));
                }

                if (filter.Column.Equals("DealerId")) 
                {
                    thisWeekAccessQuery = thisWeekAccessQuery
                        .Where(e => e.DealerId == int.Parse(filter.Value));

                    userAccessesQuery = userAccessesQuery
                        .Where(e => e.DealerId == int.Parse(filter.Value));

                    var dealerCode = _context.Dealers.Find(int.Parse(filter.Value)).DealerCode;
                    var salesCodes = _context.Salesmen
                        .Where(e => users.Contains(e.SalesmanCode))
                        .Where(e => e.DealerCode.Equals(dealerCode))
                        .Select(e => e.SalesmanCode)
                        .ToList();
                    pktSubmittedQuery = pktSubmittedQuery
                        .Where(e => salesCodes.Contains(e.SalesCode));
                }

                if (filter.Column.Equals("CityId")) 
                {
                    thisWeekAccessQuery = thisWeekAccessQuery
                        .Where(e => e.CityId == int.Parse(filter.Value));

                    userAccessesQuery = userAccessesQuery
                        .Where(e => e.CityId == int.Parse(filter.Value));

                    var cityName = _context.Cities.Find(Int16.Parse(filter.Value)).CityName;
                    var salesCodes = _context.Salesmen
                        .Where(e => users.Contains(e.SalesmanCode))
                        .Where(e => e.DealerCity.Equals(cityName))
                        .Select(e => e.SalesmanCode)
                        .ToList();
                    pktSubmittedQuery = pktSubmittedQuery
                        .Where(e => salesCodes.Contains(e.SalesCode));
                }

                if (filter.Column.Equals("DealerBranchId")) 
                {
                    thisWeekAccessQuery = thisWeekAccessQuery
                        .Where(e => e.DealerBranchId == int.Parse(filter.Value));

                    userAccessesQuery = userAccessesQuery
                        .Where(e => e.DealerBranchId == int.Parse(filter.Value));

                    var branchCode = _context.DealerBranches.Find(int.Parse(filter.Value)).DealerBranchCode;
                    var salesCodes = _context.Salesmen
                        .Where(e => users.Contains(e.SalesmanCode))
                        .Where(e => e.DealerBranchCode.Equals(branchCode))
                        .Select(e => e.SalesmanCode)
                        .ToList();
                    pktSubmittedQuery = pktSubmittedQuery
                        .Where(e => salesCodes.Contains(e.SalesCode));
                }
            }

            var pktSubmitted = pktSubmittedQuery
                .GroupBy(e => e.SalesCode)
                .Select(u => new {
                    SalesCode = u.Key,
                    Total = u.Count()
                })
                .OrderByDescending(e => e.Total)
                .Take(3)
                .ToList();
            
            var thisWeekAccessData = thisWeekAccessQuery
                .Where(e => e.IsNew)
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

            var userAccesses = userAccessesQuery
                .Where(e => e.IsNew)
                .GroupBy(e => e.UserId)
                .Select(u => new {
                    UserId = u.Key,
                    Total = u.Count()
                })
                .OrderByDescending(e => e.Total)
                .Take(3)
                .ToList();

            var topUserAccesses = new List<UserActiveDataItem>();
            userAccesses.ForEach(e => {
                var user = _context.Users.Find(e.UserId);
                topUserAccesses.Add(new UserActiveDataItem {
                   Id =  user.Id,
                   DealerName = user.Dealer.DealerName,
                   DealerCode = user.Dealer.DealerCode,
                   DealerCity = user.Dealer.City.CityName,
                   SalesCode = user.UserName,
                   SalesName = user.Salesman.SalesmanName,
                   TotalAccess = e.Total
                });
            });

            
            var pkt = new List<PKTDataItem>();
            pktSubmitted.ForEach(u => {
                var sales = _context.Salesmen.Where(e => e.SalesmanCode.Equals(u.SalesCode)).FirstOrDefault();
                pkt.Add(new PKTDataItem {
                    Id = sales.Id,
                    SalesCode = sales.SalesmanCode,
                    SalesName = sales.SalesmanName,
                    DealerName = sales.DealerName,
                    TotalReport = u.Total
                });
            });

            var materialAccesses = new List<MaterialAccessDataItem>();
            if (selectedUser != null) // has user filter
            {
                materialAccesses = _context.MaterialCounters
                    .Where(e => e.ContentType.Equals("bulletin") 
                        || e.ContentType.Equals("info")
                        || e.ContentType.Equals("training")
                        || e.ContentType.Equals("guide")
                    )
                    .Where(e => e.UserId == selectedUser.Id)
                    .Select(x => new {x.ContentId, x.ContentType, x.UserId})
                    .Distinct()
                    .GroupBy(e => e.ContentType)
                    .Select(u => new MaterialAccessDataItem{
                        Category = u.Key,
                        TotalViews = u.Count()
                    })
                    .OrderByDescending(e => e.TotalViews)
                    .ToList(); 
            }

            return new GetUsersReportDto()
            {
                Success = true,
                Message = "User Report data are succefully retrieved",
                Data = new GetUsersReportData {
                    TopMenu = topMenu,
                    PKTData = new PKTData {
                        Items = pkt
                    },
                    UserActive = new UserActiveData {
                        Items = topUserAccesses
                    },
                    Charts = new ChartData {
                        Items = thisWeekAccess
                    },
                    MaterialAccess = new MaterialAccessData {
                        Items = materialAccesses
                    }
                }
            };
        }
    }
}
