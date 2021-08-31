using System.Collections.Generic;
using AutoMapper;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.Interfaces.Mappings;

namespace SFIDWebAPI.Application.UseCases.Admin.Master.Queries.GetJobPositions
{
    public class GetJobPositionsDto : BaseDto
    {
        public IList<JobPositionData> Data { set; get; }
    }

    public class JobPositionData : IHaveCustomMapping
    {
        public int Id { set; get; }
        public string Description { set; get; }
        public string Code { set; get; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Entities.JobPosition, JobPositionData>()
                .ForMember(bDto => bDto.Id, opt => opt.MapFrom(b => b.Id))
                .ForMember(bDto => bDto.Description, opt => opt.MapFrom(b => b.Description))
                .ForMember(bDto => bDto.Code, opt => opt.MapFrom(b => b.Code));
        }
    }
}
