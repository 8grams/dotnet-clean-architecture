using System;
using System.Threading.Tasks;
using AutoMapper;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.Interfaces.Mappings;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Domain.Infrastructures;

namespace SFIDWebAPI.Application.UseCases.Admin.Recommendation.Models
{
    public class RecommendationData : BaseDtoData, IHaveCustomMapping
    {
        public string Type { set; get; }
        public string Title { set; get; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Infrastructures.IRecommendable, RecommendationData>()
                .ForMember(bDto => bDto.Id, opt => opt.MapFrom(b => b.Id))
                .ForMember(bDto => bDto.Title, opt => opt.MapFrom(b => b.Title));
        }

        public static async Task<IRecommendable> GetRecommendationContent(ISFDDBContext context,  string type, int id)
        {
            IRecommendable data;
            switch (type)
            {
                case "bulletin":
                    data = await context.Bulletins.FindAsync(id);
                    break;
                case "training":
                    data = await context.TrainingMaterials.FindAsync(id);
                    break;
                case "guide":
                    data = await context.GuideMaterials.FindAsync(id);
                    break;
                case "info":
                    data = await context.AdditionalInfos.FindAsync(id);
                    break;
                default:
                    throw new InvalidOperationException($"Type is undefined for {type}");
            }

            return data;
        }
    }
}