using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Interfaces.Mappings;
using SFIDWebAPI.Application.Infrastructures.AutoMapper;

namespace SFIDWebAPI.Application.UseCases.Admin.Report.Queries.TrainingMaterial
{
    public class GetTrainingMaterialReportDto : BaseDto
    {
        public TrainingMaterialReportData Data { set; get; }
    }

    public class TrainingMaterialReportData
    {
        public TopMenuData TopMenu { set; get; }
        public ChartData Charts { set; get; }
        public TopVisit TopVisit { set; get; }
        public TopVisitByType TopVisitByType { set; get; }
        public MaterialVisitDetail MaterialVisitDetail { set; get; }
    }

    public class TopMenuData
    {
        public IList<TopMenuDataItem> Items { set; get; }
    }

    public class TopMenuDataItem
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Key { set; get; }
        public string Value { set; get; }
        public string Icon { set; get; }
        public string Color { set; get; }
    }

    public class ChartData
    {
        public IList<ChartDataItem> Items { set; get; }
    }

    public class ChartDataItem
    {
        public int Value { set; get; }
        public int CategoryId { set; get; }
        public string CategoryName { set; get; }
    }

    public class TopVisit
    {
        public IList<TopVisitItem> Items { set; get; }
    }

    public class TopVisitItem
    {
        public int Id { set; get; }
        public string Title { set; get; }
        public int TotalVisit { set; get; }
    }


    public class TopVisitByType
    {
        public IList<TopVisitByTypeItem> Items { set; get; }
    }

    public class TopVisitByTypeItem
    {
        public string TypeName { set; get; }
        public int TotalVisits { set; get; }
    }
    
    public class MaterialVisitDetail
    {
        public IList<MaterialVisitItem> Items { set; get; }
        public PaginationData Pagination { set; get; }
    }

    public class MaterialVisitItem : BaseDtoData, IHaveCustomMapping
    {
        public string Title { set; get; }
        public string Category { set; get; }
        public string CarType { set; get; }
        public string TotalViews { set; get; }
        public string TotalUserVisited { set; get; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Entities.TrainingMaterial, MaterialVisitItem>()
                .ForMember(bDto => bDto.Id, opt => opt.MapFrom(b => b.Id))
                .ForMember(bDto => bDto.Title, opt => opt.MapFrom(b => b.Title))
                .ForMember(bDto => bDto.Category, opt => opt.MapFrom(b => b.MasterCar.Title))
                .ForMember(bDto => bDto.CarType, opt => opt.MapFrom(b => b.MasterCar.Tag))
                .ForMember(bDto => bDto.TotalViews, opt => opt.MapFrom(b => b.TotalViews))
                .ForMember(bDto => bDto.TotalUserVisited, opt => opt.MapFrom(b => ResolveTotalUserVisited(((AutoMapperProfile)configuration)._context, b)));
        }

        public static int ResolveTotalUserVisited(ISFDDBContext context, Domain.Entities.TrainingMaterial source)
        {
            var total = context.MaterialCounters
                .Where(e => e.ContentId == source.Id)
                .Where(e => e.ContentType.Equals(Domain.Entities.Recommendation.TYPE_TRAINING))
                .Select(x => new {x.ContentId, x.ContentType, x.UserId})
                .Distinct().Count();
            return total;
        }
    }
}
