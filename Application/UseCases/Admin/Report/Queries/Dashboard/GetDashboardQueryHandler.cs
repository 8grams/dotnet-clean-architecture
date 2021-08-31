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

namespace SFIDWebAPI.Application.UseCases.Admin.Report.Queries.Dashboard
{
    public class GetDashboardQueryHandler : IRequestHandler<GetDashboardQuery, GetDashboardDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public GetDashboardQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetDashboardDto> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
        {
            // top menu //
            // active account
            var totalAccounts =  await _context.Salesmen
                .Where(e => e.JobDescription.Equals("Salesman"))
                .CountAsync();
            var totalRegistered = await _context.Users.CountAsync();

            // current access
            var accessed = _context.UserPresences
                .Where(e => e.CreateDate >= (DateTime.Now.AddMinutes(-10)))
                .Select(e => new { 
                    UserId = e.UserId, 
                    Platform = e.Platform 
                })
                .Distinct()
                .GroupBy(e => e.Platform)
                .Select(u => new {
                    Platform = u.Key,
                    Total = u.Count()
                })
                .ToList();

            var iosAccess = 0;
            var androidAccess = 0;
            accessed.ForEach(e => {
                if (e.Platform.Equals("android")) {
                    androidAccess = e.Total;
                } else {
                    iosAccess = e.Total;
                }
            });
            var totalPlatformAccess = iosAccess + androidAccess;

            // PKT Submitted
            var pkt = await _context.PKTHistories.Where(e => e.Status == 1).CountAsync();

            var topMenu = new TopMenuData {
                Items = new List<TopMenuDataItem> {
                    new TopMenuDataItem {
                        Id = 1,
                        Name = "Active Account",
                        Key = "users",
                        Value = totalRegistered.ToString() + "/" + totalAccounts.ToString(),
                        Icon = "list-alt",
                        Color = "#01CAB9"
                    },
                    new TopMenuDataItem {
                        Id = 2,
                        Name = "Current Access",
                        Key = "users",
                        Value = totalPlatformAccess.ToString(),
                        Icon = "unlock-alt",
                        Color = "#CA5501"
                    },
                    new TopMenuDataItem {
                        Id = 3,
                        Name = "iOS Access",
                        Key = "users",
                        Value = iosAccess.ToString(),
                        Icon = "iOS",
                        Color = "#CA0176"
                    },
                    new TopMenuDataItem {
                        Id = 4,
                        Name = "Android Access",
                        Key = "users",
                        Value = androidAccess.ToString(),
                        Icon = "android",
                        Color = "#CAB901"
                    },
                    new TopMenuDataItem {
                        Id = 5,
                        Name = "PKT Submitted",
                        Key = "files",
                        Value = pkt.ToString(),
                        Icon = "reply",
                        Color = "#12CA01"
                    },
                }
            };

            // category materials //
            // bulletin
            var topBulletins = _context.Bulletins
                .Select(bulletin => new CategoryMaterialItem
                {
                    Id = bulletin.Id,
                    Title = bulletin.Title,
                    Category = "bulletin",
                    TotalViews = bulletin.TotalViews
                });

            var topInfos = _context.AdditionalInfos
                .Select(info => new CategoryMaterialItem
                {
                    Id = info.Id,
                    Title = info.Title,
                    Category = "info",
                    TotalViews = info.TotalViews
                });

            var topTrainings = _context.TrainingMaterials
                .Select(training => new CategoryMaterialItem
                {
                    Id = training.Id,
                    Title = training.Title,
                    Category = "training",
                    TotalViews = training.TotalViews
                });

            var topGuides = _context.GuideMaterials
                .Select(guide => new CategoryMaterialItem
                {
                    Id = guide.Id,
                    Title = guide.Title,
                    Category = "guide",
                    TotalViews = guide.TotalViews
                });

            var topMaterialsData = await topBulletins
                .Union(topInfos)
                .Union(topTrainings)
                .Union(topGuides)
                .OrderByDescending(e => e.TotalViews)
                .Take(3)
                .ToListAsync();

            // set total user accessed
            var topMaterials = new List<CategoryMaterialItem>();
            foreach (var data in topMaterialsData)
            {
                var md = data;
                var totalAccess = _context.MaterialCounters
                    .Where(e => e.ContentType.Equals(data.Category))
                    .Where(e => e.ContentId == data.Id)
                    .Select(x => new {x.ContentId, x.ContentType, x.UserId})
                    .Distinct().Count();
                md.TotalUserAccessed = totalAccess;
                topMaterials.Add(md);
            }
            // ordering
            topMaterials.OrderByDescending(e => e.TotalViews).ThenByDescending(e => e.TotalUserAccessed);

            // Active users //
            var activeDealers = _context.UserPresences
                .GroupBy(e => e.DealerId)
                .Select(u => new {
                    DealerId = (Int16) u.Key,
                    Total = u.Count()
                })
                .OrderByDescending(e => e.Total)
                .Take(10)
                .ToList();

            var activeUsers = new List<UserActiveDataItem>();
            activeDealers.Take(3).ToList().ForEach(e => {
                var idDealer = (int) e.DealerId;
                var dealer = _context.Dealers.Find(idDealer);
                if (dealer != null) {
                    activeUsers.Add(new UserActiveDataItem {
                        Id = dealer.Id,
                        DealerCity = dealer.City.CityName,
                        DealerCode = dealer.DealerCode,
                        DealerGroup = dealer.DealerGroup.GroupName,
                        DealerName = dealer.DealerName,
                        TotalAccess = e.Total // fix here
                    });
                }
            });

            // chart data
            var chartDataItems = new List<ChartDataItem>();
            activeDealers.ForEach(e => {
                var idDealer = (int) e.DealerId;
                var dealer = _context.Dealers.Find(idDealer);
                if (dealer != null) {
                    chartDataItems.Add(new ChartDataItem {
                        Value = e.Total,
                        DealerCode = dealer.DealerCode,
                        DealerName = dealer.DealerName,
                        DealerCity = dealer.City.CityName
                    });
                }
            });
            
            return new GetDashboardDto()
            {
                Success = true,
                Message = "Dashboard data are succefully retrieved",
                Data = new DashboardData {
                    CategoryMaterials = new CategoryMaterialData { Items = topMaterials },
                    TopMenu = topMenu,
                    UserActive = new UserActiveData {
                        Items = activeUsers
                    },
                    Charts = new ChartData {
                        Items = chartDataItems
                    }
                }
            };
        }
    }
}
