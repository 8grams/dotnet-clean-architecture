using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Domain.Infrastructures;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.Misc;

namespace SFIDWebAPI.Application.UseCases.User.Recommendation.Queries.GetRecommendationList
{
    public class RecommendationListQueryHandler : IRequestHandler<RecommendationListQuery, RecommendationListDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;
        private readonly Utils _utils;

        public RecommendationListQueryHandler(ISFDDBContext context, IMapper mapper, Utils utils)
        {
            _context = context;
            _mapper = mapper;
            _utils = utils;
        }

        public async Task<RecommendationListDto> Handle(RecommendationListQuery request, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;
            var records = await _context.Recommendations
                .Where(e => e.PublishedAt < now)
                .Where(e => e.ExpiresAt > now)
                .ToListAsync();
                
            var recommendations = new List<RecommendationData>();

            if (records.Count > 0)
            {
                foreach(var data in records)
                {
                    var recommendable = await this.GetRecommendationContent(data.ContentType, data.ContentId);
                    if (recommendable != null) 
                    {
                        recommendations.Add(new RecommendationData
                        {
                            ContentId = data.ContentId,
                            ContentType = data.ContentType,
                            Title = recommendable.Title,
                            Type = recommendable.FileType,
                            Link = _utils.GetValidUrl(recommendable.Link),
                            PublishedAt = recommendable.PublishedAt,
                            CreatedAt = recommendable.CreateDate,
                            TotalViews = recommendable.TotalViews,
                            Images = new ImageDto
                            {
                                Cover = _utils.GetValidUrl(recommendable.ImageThumbnail.Link),
                                Thumbnail = _utils.GetValidUrl(recommendable.ImageThumbnail.Link)
                            },
                            Source = "recommendation"
                        });
                    }
                }
            }

            if (recommendations.Count < 10) {
                var materials = await _context.TrainingMaterials
                    .Include(e => e.ImageThumbnail)
                    .Where(e => e.PublishedAt < now)
                    .Where(e => e.ExpiresAt > now)
                    .ToListAsync();

                foreach(var data in materials)
                {
                    var isExist = recommendations
                        .Where(e => e.ContentId == data.Id)
                        .Where(e => e.ContentType.Equals("training"))
                        .FirstOrDefault();
                    if (isExist != null) continue;

                    recommendations.Add(new RecommendationData
                    {
                        ContentId = data.Id,
                        ContentType = "training",
                        Title = data.Title,
                        Type = data.FileType,
                        Link = _utils.GetValidUrl(data.Link),
                        PublishedAt = data.PublishedAt,
                        CreatedAt = data.CreateDate,
                        TotalViews = data.TotalViews,
                        Images = new ImageDto
                        {
                            Cover = _utils.GetValidUrl(data.ImageThumbnail.Link),
                            Thumbnail = _utils.GetValidUrl(data.ImageThumbnail.Link)
                        },
                        Source = "training"
                    });

                    if (recommendations.Count >= 10) break;
                }
            }

            if (recommendations.Count < 10) {
                var materials = await _context.GuideMaterials
                    .Include(e => e.ImageThumbnail)
                    .Where(e => e.PublishedAt < now)
                    .Where(e => e.ExpiresAt > now)
                    .ToListAsync();

                foreach(var data in materials)
                {
                    var isExist = recommendations
                        .Where(e => e.ContentId == data.Id)
                        .Where(e => e.ContentType.Equals("guide"))
                        .FirstOrDefault();
                    if (isExist != null) continue;
                    
                    recommendations.Add(new RecommendationData
                    {
                        ContentId = data.Id,
                        ContentType = "guide",
                        Title = data.Title,
                        Type = data.FileType,
                        Link = _utils.GetValidUrl(data.Link),
                        PublishedAt = data.PublishedAt,
                        CreatedAt = data.CreateDate,
                        TotalViews = data.TotalViews,
                        Images = new ImageDto
                        {
                            Cover = _utils.GetValidUrl(data.ImageThumbnail.Link),
                            Thumbnail = _utils.GetValidUrl(data.ImageThumbnail.Link)
                        },
                        Source = "guide"
                    });

                    if (recommendations.Count >= 10) break;
                }
            }

            if (recommendations.Count < 10) {
                var materials = await _context.Bulletins
                    .Include(e => e.ImageThumbnail)
                    .Where(e => e.PublishedAt < now)
                    .Where(e => e.ExpiresAt > now)
                    .ToListAsync();

                foreach(var data in materials)
                {
                    var isExist = recommendations
                        .Where(e => e.ContentId == data.Id)
                        .Where(e => e.ContentType.Equals("bulletin"))
                        .FirstOrDefault();
                    if (isExist != null) continue;

                    recommendations.Add(new RecommendationData
                    {
                        ContentId = data.Id,
                        ContentType = "bulletin",
                        Title = data.Title,
                        Type = data.FileType,
                        Link = _utils.GetValidUrl(data.Link),
                        PublishedAt = data.PublishedAt,
                        CreatedAt = data.CreateDate,
                        TotalViews = data.TotalViews,
                        Images = new ImageDto
                        {
                            Cover = _utils.GetValidUrl(data.ImageThumbnail.Link),
                            Thumbnail = _utils.GetValidUrl(data.ImageThumbnail.Link)
                        },
                        Source = "bulletin"
                    });

                    if (recommendations.Count >= 10) break;
                }
            }

            if (recommendations.Count < 10) {
                var materials = await _context.AdditionalInfos
                    .Include(e => e.ImageThumbnail)
                    .Where(e => e.PublishedAt < now)
                    .Where(e => e.ExpiresAt > now)
                    .ToListAsync();

                foreach(var data in materials)
                {
                    var isExist = recommendations
                        .Where(e => e.ContentId == data.Id)
                        .Where(e => e.ContentType.Equals("info"))
                        .FirstOrDefault();
                    if (isExist != null) continue;

                    recommendations.Add(new RecommendationData
                    {
                        ContentId = data.Id,
                        ContentType = "info",
                        Title = data.Title,
                        Type = data.FileType,
                        Link = _utils.GetValidUrl(data.Link),
                        PublishedAt = data.PublishedAt,
                        CreatedAt = data.CreateDate,
                        TotalViews = data.TotalViews,
                        Images = new ImageDto
                        {
                            Cover = _utils.GetValidUrl(data.ImageThumbnail.Link),
                            Thumbnail = _utils.GetValidUrl(data.ImageThumbnail.Link)
                        },
                        Source = "info"
                    });

                    if (recommendations.Count >= 10) break;
                }
            }

            recommendations = recommendations.OrderByDescending(e => e.CreatedAt).ToList();
            var results = new List<RecommendationData>();
            
            // set is downloable
            foreach (var item in recommendations)
            {
                if (item.ContentType == "info")
                {
                    var aInfo = await _context.AdditionalInfos.FindAsync(item.ContentId);
                    if (aInfo != null && aInfo.IsDownloadable)
                    {
                        item.IsDownloadable = true;
                    }
                }
                else
                {
                    item.IsDownloadable = false;
                }
                results.Add(item);
            }

            return new RecommendationListDto()
            {
                Success = true,
                Message = "Recommendations are succefully retrieved",
                Data = results
            };
        }

        private async Task<IRecommendable> GetRecommendationContent(string type, int id)
        {
            IRecommendable data;
            switch (type)
            {
                case "bulletin":
                    data = await _context.Bulletins
                        .Include(e => e.ImageThumbnail)
                        .Where(e => e.Id == id)
                        .FirstOrDefaultAsync();
                    break;
                case "training":
                    data = await _context.TrainingMaterials
                        .Include(e => e.ImageThumbnail)
                        .Where(e => e.Id == id)
                        .FirstOrDefaultAsync();
                    break;
                case "guide":
                    data = await _context.GuideMaterials
                        .Include(e => e.ImageThumbnail)
                        .Where(e => e.Id == id)
                        .FirstOrDefaultAsync();
                    break;
                case "info":
                    data = await _context.AdditionalInfos
                        .Include(e => e.ImageThumbnail)
                        .Where(e => e.Id == id)
                        .FirstOrDefaultAsync();
                    break;
                default:
                    throw new InvalidOperationException("Type is undefined");
            }

            return data;
        }
    }
}
