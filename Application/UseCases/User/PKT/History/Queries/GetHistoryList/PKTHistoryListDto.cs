using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using StoredProcedureEFCore;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Interfaces.Mappings;
using SFIDWebAPI.Application.UseCases.User.PKT.Models;
using SFIDWebAPI.Application.Infrastructures.AutoMapper;

namespace SFIDWebAPI.Application.UseCases.User.PKT.History.Queries.GetHistoryList
{
    public class PKTHistoryListDto : PaginationDto
    {
        public IList<PKTHistoryData> Data { set; get; }
    }

    public class PKTHistoryData : BaseDtoData, IHaveCustomMapping
    {
        public VehicleInfo VehicleInfo { set; get; }
        public SalesInfo SalesInfo { set; get; }
        public DateTime PDIDate { set; get; }
        public string Status { set; get; }
        public string Type { set; get; }
        public string Note { set; get; }
        public string Image { set; get; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Entities.PKTHistory, PKTHistoryData>()
                .ForMember(nDto => nDto.Id, opt => opt.MapFrom(n => n.Id))
                .ForMember(nDto => nDto.VehicleInfo, opt => opt.MapFrom(n => ResolveVinData(((AutoMapperProfile)configuration)._context, n)))
                .ForMember(nDto => nDto.SalesInfo, opt => opt.MapFrom(n => new SalesInfo
                {
                    Name = n.Salesman.SalesmanName,
                    SalesCode = n.Salesman.SalesmanCode,
                    DealerName = n.Salesman.DealerName
                }))
                .ForMember(nDto => nDto.Status, opt => opt.MapFrom(n => ResolveStatus(n.Status)))
                .ForMember(nDto => nDto.Type, opt => opt.MapFrom(n => n.ReportType))
                .ForMember(nDto => nDto.Note, opt => opt.MapFrom(n => n.Note))
                .ForMember(nDto => nDto.Image, opt => opt.MapFrom(n => ((AutoMapperProfile)configuration).GetFullUrl(n.Image)))
                .ForMember(nDto => nDto.PDIDate, opt => opt.MapFrom(n => n.PDIDate))
                .ForMember(nDto => nDto.CreatedAt, opt => opt.MapFrom(n => n.CreateDate))
                .ForMember(nDto => nDto.UpdatedAt, opt => opt.MapFrom(n => n.LastUpdateDate));
        }

        public static VehicleInfo ResolveVinData(ISFDDBContext context, Domain.Entities.PKTHistory source)
        {
            List<VinDataSP> spData = null;
            context.loadStoredProcedureBuilder("Sp_GetVinNumber")
                .AddParam("vinNumber", source.Vin)
                .Exec(r => spData = r.ToList<VinDataSP>());

            VinDataSP vinData = null;
            vinData = spData.FirstOrDefault();

            if (vinData == null)
            {
                return null;
            }
            
            return new VehicleInfo
            {
                Vin = vinData.ChassisNumber,
                VehicleColor = vinData.VehicleColor,
                VehicleType = vinData.Description
            };

        }

        public static String ResolveStatus(int status)
        {
            var res = "success";
            if (status == 0)
            {
                res = "draft";
            }
            else if (status == 1)
            {
                res = "success";
            }
            else 
            {
                res = "failed";
            }
            return res;
        }
    }
}
