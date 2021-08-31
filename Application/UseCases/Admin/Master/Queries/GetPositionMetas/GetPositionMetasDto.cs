using System.Collections.Generic;
using AutoMapper;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.Interfaces.Mappings;
using SFIDWebAPI.Domain.Entities;

namespace SFIDWebAPI.Application.UseCases.Admin.Master.Queries.GetPositionMetas
{
    public class GetPositionMetasDto : BaseDto
    {
        public IList<PositionMetaData> Data { set; get; }
    }
    
    public class PositionData : JobPosition {

    }

    public class PositionMetaData : IHaveCustomMapping
    {
        public int Id { set; get; }
        public string Description { set; get; }
        public string Code { set; get; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<PositionData, PositionMetaData>()
                .ForMember(bDto => bDto.Id, opt => opt.MapFrom(b => b.Id))
                .ForMember(bDto => bDto.Description, opt => opt.MapFrom(b => b.Description))
                .ForMember(bDto => bDto.Code, opt => opt.MapFrom(b => b.Code));
        }
    }
}
