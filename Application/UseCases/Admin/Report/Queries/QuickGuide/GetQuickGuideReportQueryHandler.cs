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
using SFIDWebAPI.Application.Extensions;

namespace SFIDWebAPI.Application.UseCases.Admin.Report.Queries.QuickGuide
{
    public class GetQuickGuideReportQueryHandler : IRequestHandler<GetQuickGuideReportQuery, GetQuickGuideReportDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public GetQuickGuideReportQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetQuickGuideReportDto> Handle(GetQuickGuideReportQuery request, CancellationToken cancellationToken)
        {
            // top menu //
            var totalVisitCategory = await _context.MaterialCounters
                .Where(e => e.ContentType.Equals("guide-category"))
                .CountAsync();
            var totalFiles = await _context.GuideMaterials.CountAsync();
            
            var totalFilesVisited = await _context.MaterialCounters
                .Where(e => e.ContentType.Equals("guide"))
                .Select(e => new { e.ContentType, e.ContentId })
                .CountAsync();

            var topMenu = new TopMenuData {
                Items = new List<TopMenuDataItem> {
                    new TopMenuDataItem {
                        Id = 1,
                        Name = "Total Visit Category",
                        Key = "total_visit",
                        Value = totalVisitCategory.ToString(),
                        Color = "#01CAB9"
                    },
                    new TopMenuDataItem {
                        Id = 2,
                        Name = "Total File",
                        Key = "total_file",
                        Value = totalFiles.ToString(),
                        Color = "#CA5501"
                    },
                    new TopMenuDataItem {
                        Id = 3,
                        Name = "Total File Visited",
                        Key = "total_file_visited",
                        Value = totalFilesVisited.ToString(),
                        Color = " #CA0176"
                    }
                }
            };

            var categoryVisitedQuery = _context.MaterialCounters
                .Where(e => e.ContentType.Equals("guide-category"))
                .Where(e => e.CreateDate >= request.StartDate)
                .Where(e => e.CreateDate <= request.EndDate);

            var guidesQuery = _context.MaterialCounters
                .Where(e => e.ContentType.Equals("guide"))
                .Where(e => e.CreateDate >= request.StartDate)
                .Where(e => e.CreateDate <= request.EndDate);

            var userAccessQuery = _context.MaterialCounters
                .Where(e => e.ContentType.Equals("guide"))
                .Where(e => e.CreateDate >= request.StartDate)
                .Where(e => e.CreateDate <= request.EndDate);

            var usersGuide = _context.MaterialCounters
                .Where(e => e.ContentType.Equals("guide"))
                .Select(e => e.UserId)
                .Distinct()
                .ToList();

            var users = _context.Users
                .Where(e => usersGuide.Contains(e.Id))
                .Select(e => e.UserName)
                .ToList();

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

                    categoryVisitedQuery = categoryVisitedQuery
                        .Where(e => usersInvolved.Contains(e.UserId));

                    guidesQuery = guidesQuery
                        .Where(e => usersInvolved.Contains(e.UserId));

                    userAccessQuery = userAccessQuery
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

                    categoryVisitedQuery = categoryVisitedQuery
                        .Where(e => usersInvolved.Contains(e.UserId));

                    guidesQuery = guidesQuery
                        .Where(e => usersInvolved.Contains(e.UserId));

                    userAccessQuery = userAccessQuery
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

                    categoryVisitedQuery = categoryVisitedQuery
                        .Where(e => usersInvolved.Contains(e.UserId));

                    guidesQuery = guidesQuery
                        .Where(e => usersInvolved.Contains(e.UserId));

                    userAccessQuery = userAccessQuery
                        .Where(e => usersInvolved.Contains(e.UserId));
                }

                if (filter.Column.Equals("CategoryId")) 
                {
                    categoryFilter = int.Parse(filter.Value);
                }
            }

            var categoryVisitedDataList = categoryVisitedQuery
                .Join(
                    _context.MasterCars,
                    p => p.ContentId,
                    q => q.Id,
                    (p, q) => new {
                        MasterCarId = q.Id
                    }
                )
                .GroupBy(e => e.MasterCarId)
                .Select(e => new {
                    CategoryId = e.Key,
                    TotalVisits = e.Count()
                });

            if (categoryFilter > 0)
            {
                categoryVisitedDataList = categoryVisitedDataList.Where(e => e.CategoryId == categoryFilter);
            }
             
            var categoryVisitedData = categoryVisitedDataList
                .OrderByDescending(e => e.TotalVisits)
                // .Take(3)
                .ToList();

            var categoryVisits = new List<TopVisitItem>();
            categoryVisitedData.ForEach(e => {
                var category = _context.MasterCars.Find(e.CategoryId);
                categoryVisits.Add(new TopVisitItem {
                    Id = category.Id,
                    Title = category.Title,
                    TotalVisit = e.TotalVisits
                });
            });

            var guidesList = guidesQuery
                .Join(
                    _context.GuideMaterials,
                    p => p.ContentId,
                    q => q.Id,
                    (p, q) => new {
                        CategoryId = q.MasterCarId,
                        FileType = q.FileType,
                        TotalViews = q.TotalViews
                    }
                );

            if (categoryFilter > 0)
            {
                guidesList = guidesList.Where(e => e.CategoryId == categoryFilter);
            }
            var guides = guidesList.ToList();

            var totalVideo = 0;
            var totalDocs = 0;

            guides.ForEach(e => {
                if (e.FileType.Equals("mp4"))
                {
                    totalVideo = totalVideo + 1;
                }
                else
                {
                    totalDocs = totalDocs + 1;
                }
            });

            var userAccessDataList = userAccessQuery
                .Join(
                    _context.GuideMaterials,
                    p => p.ContentId,
                    q => q.Id,
                    (p, q) => new {
                        MasterCarId = q.MasterCarId,
                        TotalViews = q.TotalViews
                    }
                )
                .GroupBy(e => e.MasterCarId)
                .Select(e => new {
                    CategoryId = e.Key,
                    TotalVisits = e.Count()
                });

            if (categoryFilter > 0)
            {
                userAccessDataList = userAccessDataList.Where(e => e.CategoryId == categoryFilter);
            }
            var userAccessData = userAccessDataList
                .OrderByDescending(e => e.TotalVisits)
                .ToList();

            var userAccesses = new List<ChartDataItem>();
            userAccessData.ForEach(e => {
                var category = _context.MasterCars.Find(e.CategoryId);
                userAccesses.Add(new ChartDataItem {
                    CategoryId = category.Id,
                    CategoryName = category.Title,
                    Value = e.TotalVisits
                });
            });

            // material visit details
            // get top 
            var results = await _context.GuideMaterials.OrderBy($"{request.SortColumn} {request.SortType}")
                .GetPagedAsync<Domain.Entities.GuideMaterial, MaterialVisitItem>(request.PagingPage, request.PagingLimit, _mapper);
            
            return new GetQuickGuideReportDto()
            {
                Success = true,
                Message = "QuickGuide Report data are succefully retrieved",
                Data = new QuickGuideReportData {
                    TopMenu = topMenu,
                    Charts = new ChartData {
                        Items = userAccesses
                    },
                    TopVisit = new TopVisit {
                        Items = categoryVisits
                    },
                    TopVisitByType = new TopVisitByType() {
                        Items = new List<TopVisitByTypeItem>() {
                            new TopVisitByTypeItem()
                            {
                                TypeName = "video",
                                TotalVisits = totalVideo
                            },
                            new TopVisitByTypeItem()
                            {
                                TypeName = "docs",
                                TotalVisits = totalDocs
                            }
                        }
                    },
                    MaterialVisitDetail = new MaterialVisitDetail
                    {
                        Items = results.Data,
                        Pagination = results.Pagination
                    }
                }
            };
        }
    }
}
