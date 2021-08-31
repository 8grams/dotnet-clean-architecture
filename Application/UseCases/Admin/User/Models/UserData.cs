using System;
using System.Linq;
using AutoMapper;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.Interfaces.Mappings;

namespace SFIDWebAPI.Application.UseCases.Admin.User.Models 
{
    public class UserData : BaseDtoData, IHaveCustomMapping 
    {
        public string Email { set; get; }
        public string Phone { set; get; }
        public string UserName { set; get; }
        public bool IsLogin { set; get; }
        public bool IsActive { set; get; }
        public DateTime? LastLogin { set; get; }
        public bool IsRegistered { set; get; }
        public string Photo { set; get; }
        public string From { set; get; }
        public GradeData Grade { set; get; }
        public MasterConfigData MasterConfig { set; get; }
        public SalesmanData Salesman { set; get; }

        public void CreateMappings (Profile configuration) 
        {
            var currentYear = (Int16) DateTime.Now.Year;
            var lastYear = currentYear - 1;

            configuration.CreateMap<Domain.Entities.SalesmanMeta, UserData>()
                .ForMember (bDto => bDto.Id, opt => opt.MapFrom (b => b.User.Id))
                .ForMember (bDto => bDto.Email, opt => opt.MapFrom (b => b.User.Email))
                .ForMember (bDto => bDto.UserName, opt => opt.MapFrom (b => b.User.UserName))
                .ForMember (bDto => bDto.Phone, opt => opt.MapFrom (b => b.User.Phone))
                .ForMember (bDto => bDto.IsLogin, opt => opt.MapFrom (b => b.User.LastLogin > (DateTime.Now.AddHours(-24)) ))
                .ForMember (bDto => bDto.IsActive, opt => opt.MapFrom (b => b.User.IsActive ))
                .ForMember (bDto => bDto.LastLogin, opt => opt.MapFrom (b => b.User.LastLogin))
                .ForMember (bDto => bDto.IsRegistered, opt => opt.MapFrom (b => b.User.IsRegistered))
                .ForMember (bDto => bDto.Photo, opt => opt.MapFrom (b => b.User.ProfilePhoto))
                .ForMember (bDto => bDto.Salesman, opt => opt.MapFrom (b => new SalesmanData
                {
                    SalesmanCode = b.SalesmanCode,
                    DealerCode = b.DealerBranch.Dealer.DealerCode,
                    DealerName = b.DealerBranch.Dealer.DealerName,
                    DealerCity = b.DealerBranch.City.CityName,
                    DealerGroup = b.DealerBranch.Dealer.DealerGroup.GroupName,
                    DealerArea = "",
                    JobPositionId = b.JobPositionId,
                    DealerBranchId = b.DealerBranchId,
                    DealerBranchCode = b.DealerBranch.DealerBranchCode,
                    DealerBranchName = b.DealerBranch.Name,
                    SalesmanName = b.SalesmanName,
                    SalesmanHireDate = b.SalesmanHireDate,
                    JobDescription = b.JobPosition.Description,
                    LevelDescription = b.SalesmanLevel.Description,
                    SuperiorCode = "",
                    SuperiorName = b.SuperiorName,
                    SalesmanEmail = b.SalesmanEmail,
                    SalesmanHandphone = b.SalesmanHandphone,
                    SalesmanStatus = "",
                    SalesmanTeamCategory = "",
                    LastUpdateTime = (DateTime) b.User.LastUpdateDate
                }))
                .ForMember(uDto => uDto.Grade, opt => opt.MapFrom(u => new GradeData
                {
                    LastYear = ResolveUserGrade(u.User, lastYear),
                    CurrentYear = ResolveUserGrade(u.User, currentYear),
                }))
                .ForMember(bDto => bDto.From, opt => opt.MapFrom(b => "sfd"))
                .ForMember(bDto => bDto.CreatedAt, opt => opt.MapFrom(b => b.User.CreateDate));

            configuration.CreateMap<Domain.Entities.Salesman, UserData>()
                .ForMember (bDto => bDto.Id, opt => opt.MapFrom (b => b.User.Id))
                .ForMember (bDto => bDto.UserName, opt => opt.MapFrom (b => b.User.UserName))
                .ForMember (bDto => bDto.Email, opt => opt.MapFrom (b => b.User.Email))
                .ForMember (bDto => bDto.Phone, opt => opt.MapFrom (b => b.User.Phone))
                .ForMember (bDto => bDto.IsLogin, opt => opt.MapFrom (b => b.User.LastLogin > (DateTime.Now.AddHours(-24)) ))
                .ForMember (bDto => bDto.IsActive, opt => opt.MapFrom (b => b.User.IsActive ))
                .ForMember (bDto => bDto.LastLogin, opt => opt.MapFrom (b => b.User.LastLogin))
                .ForMember (bDto => bDto.IsRegistered, opt => opt.MapFrom (b => b.User.IsRegistered))
                .ForMember (bDto => bDto.Photo, opt => opt.MapFrom (b => b.User.ProfilePhoto))
                .ForMember (bDto => bDto.Salesman, opt => opt.MapFrom (b => new SalesmanData
                {
                    SalesmanCode = b.SalesmanCode,
                    DealerCode = b.DealerCode,
                    DealerName = b.DealerName,
                    DealerCity = b.DealerCity,
                    DealerGroup = b.DealerGroup,
                    DealerArea = b.DealerArea,
                    DealerBranchCode = b.DealerBranchCode,
                    DealerBranchName = b.DealerBranchName,
                    SalesmanName = b.SalesmanName,
                    SalesmanHireDate = b.SalesmanHireDate,
                    JobDescription = b.JobDescription,
                    LevelDescription = b.LevelDescription,
                    SuperiorCode = b.SuperiorCode,
                    SuperiorName = b.SuperiorName,
                    SalesmanEmail = b.SalesmanEmail,
                    SalesmanHandphone = b.SalesmanHandphone,
                    SalesmanStatus = b.SalesmanStatus,
                    SalesmanTeamCategory = b.SalesmanTeamCategory,
                    LastUpdateTime = b.LastUpdateTime,
                }))
                .ForMember(uDto => uDto.Grade, opt => opt.MapFrom(u => new GradeData
                {
                    LastYear = ResolveUserGrade(u.User, lastYear),
                    CurrentYear = ResolveUserGrade(u.User, currentYear),
                }))
                .ForMember(bDto => bDto.From, opt => opt.MapFrom(b => "dnet"))
                .ForMember(bDto => bDto.CreatedAt, opt => opt.MapFrom(b => b.User.CreateDate));
        }

        public static string ResolveUserGrade(Domain.Entities.User source, int year)
        {
            var grade = source.Salesman.SalesmanGrades.Where(e => e.Year == year).LastOrDefault();
            if (grade == null) 
            {
                return "1";
            }

            return grade.Grade.ToString();
        }
    }

    public class MasterConfigData {
        public string Category { set; get; }
        public string ValueId { set; get; }
        public string ValueCode { set; get; }
        public string ValueDesc { set; get; }
        public int Sequence { set; get; }
    }

    public class SalesmanData {
        public string DealerCode { set; get; }
        public string DealerName { set; get; }
        public string DealerCity { set; get; }
        public string DealerGroup { set; get; }
        public string DealerArea { set; get; }
        public int? DealerBranchId { set; get; }
        public int? JobPositionId { set; get; }
        public string DealerBranchCode { set; get; }
        public string DealerBranchName { set; get; }
        public string SalesmanCode { set; get; }
        public string SalesmanName { set; get; }
        public DateTime SalesmanHireDate { set; get; }
        public string JobDescription { set; get; }
        public string LevelDescription { set; get; }
        public string SuperiorName { set; get; }
        public string SuperiorCode { set; get; }
        public string SalesmanEmail { set; get; }
        public string SalesmanHandphone { set; get; }
        public string SalesmanTeamCategory { set; get; }
        public string SalesmanStatus { set; get; }
        public DateTime LastUpdateTime { set; get; }
        public GradeData Grade { set; get; }
    }
}