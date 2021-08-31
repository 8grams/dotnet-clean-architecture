using System;
using System.Collections.Generic;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Recommendation.Queries.GetRecommendationList
{
    public class RecommendationListDto : BaseDto
    {
        public IList<RecommendationData> Data { set; get; }
    }

    public class RecommendationData : BaseDtoData
    {
        public string ContentType { set; get; }
        public int ContentId { set; get; }
        public string Title { set; get; }
        public string Link { set; get; }
        public string Type { set; get; }

        public string Source { set; get; }
        public DateTime PublishedAt { set; get; }
        public int TotalViews { set; get; }
        public ImageDto Images { set; get; }
        public bool IsDownloadable { set; get; }
    }
}
