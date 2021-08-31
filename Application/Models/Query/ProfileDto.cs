using System;
using System.Linq;
using AutoMapper;
using SFIDWebAPI.Domain.Entities;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Interfaces.Mappings;
using SFIDWebAPI.Application.Infrastructures.AutoMapper;

namespace SFIDWebAPI.Application.Models.Query
{
    public class ProfileDto : IHaveCustomMapping
    {
        public int Id;
        public bool IsRegistered;
        public ProfileData Profile { set; get; }
        public GradeData Grade { set; get; }
        public TeamData Team { set; get; }
        public DealerData Dealer { set; get; }
        public AnalyticData Analytic { set; get; }
        public OTPData AccessToken { set; get; }
        public bool isDataMatch { set; get; }

        public void CreateMappings(Profile configuration)
        {
            var currentYear = (Int16) DateTime.Now.Year;
            var lastYear = currentYear - 1;
            configuration.CreateMap<User, ProfileDto>()
                .ForMember(uDto => uDto.Id, opt => opt.MapFrom(u => u.Id))
                .ForMember(uDto => uDto.IsRegistered, opt => opt.MapFrom(u => u.IsRegistered))
                .ForMember(uDto => uDto.isDataMatch, opt => opt.MapFrom(u => u.Email == u.Salesman.SalesmanEmail && u.Phone == u.Salesman.SalesmanHandphone))
                .ForMember(uDto => uDto.Profile, opt => opt.MapFrom(u => new ProfileData
                {
                    Email = u.Email,
                    Phone = u.Phone,
                    Name = u.Salesman.SalesmanName,
                    SalesmanEmail = u.Salesman.SalesmanEmail,
                    SalesmanPhone = u.Salesman.SalesmanHandphone,
                    Position = u.Salesman.JobDescription,
                    Level = u.Salesman.LevelDescription,
                    SalesCode = u.Salesman.SalesmanCode,
                    Photo = ((AutoMapperProfile)configuration).GetFullUrl( string.IsNullOrEmpty(u.ProfilePhoto) ? "/Uploads/profile/sfid-default-avatar.jpg" : u.ProfilePhoto )
                }))
                .ForMember(uDto => uDto.Dealer, opt => opt.MapFrom(u => new DealerData
                {
                    Name = u.Salesman.DealerName,
                    Branch = u.Salesman.DealerBranchName,
                    City = u.Salesman.DealerCity,
                    Group = u.Salesman.DealerGroup,
                    Code = u.Salesman.DealerCode
                }))
                .ForMember(uDto => uDto.Team, opt => opt.MapFrom(u => new TeamData
                {
                    Category = u.Salesman.SalesmanTeamCategory,
                    Supervisor = u.Salesman.SuperiorName,
                    EntryDate = u.Salesman.SalesmanHireDate.ToString()
                }))
                .ForMember(uDto => uDto.Grade, opt => opt.MapFrom(u => new GradeData
                {
                    LastYear = ResolveUserGrade(u, lastYear),
                    CurrentYear = ResolveUserGrade(u, currentYear),
                }))
                .ForMember(uDto => uDto.Analytic, opt => opt.MapFrom(u => 
                    ResolveUserAnalyticData( ((AutoMapperProfile)configuration)._context, u)
                ));

            configuration.CreateMap<Salesman, ProfileDto>()
                .ForMember(uDto => uDto.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(uDto => uDto.IsRegistered, opt => opt.MapFrom(s => false))
                .ForMember(uDto => uDto.Profile, opt => opt.MapFrom(s => new ProfileData
                {
                    Email = s.SalesmanEmail,
                    Phone = s.SalesmanHandphone,
                    Name = s.SalesmanName,
                    Position = s.JobDescription,
                    Level = s.LevelDescription,
                    SalesCode = s.SalesmanCode
                }))
                .ForMember(uDto => uDto.Dealer, opt => opt.MapFrom(s => new DealerData
                {
                    Name = s.DealerName,
                    Branch = s.DealerBranchName,
                    City = s.DealerCity,
                    Group = s.DealerGroup,
                    Code = s.DealerCode

                }))
                .ForMember(uDto => uDto.Team, opt => opt.MapFrom(s => new TeamData
                {
                    Category = s.SalesmanTeamCategory,
                    Supervisor = s.SuperiorName,
                    EntryDate = s.SalesmanHireDate.ToString()
                }))
                .ForMember(uDto => uDto.Grade, opt => opt.MapFrom(u => new GradeData
                {
                    LastYear = ResolveUserGrade(u.User, lastYear),
                    CurrentYear = ResolveUserGrade(u.User, currentYear),
                }))
                .ForMember(uDto => uDto.Analytic, opt => opt.MapFrom(u => 
                    ResolveSalesmanAnalyticData( ((AutoMapperProfile)configuration)._context, u)
                ));
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

        public static AnalyticData ResolveSalesmanAnalyticData(ISFDDBContext context, Domain.Entities.Salesman salesman)
        {
            var jobPosition = context.JobPositions
                .Where(e => e.Description.Equals(salesman.JobDescription))
                .FirstOrDefault();
            var jobPositionId = 0;
            if (jobPosition != null) jobPositionId = jobPosition.Id;

            var dealer = context.Dealers
                .Where(e => e.DealerCode.Equals(salesman.DealerCode))
                .FirstOrDefault();
            var dealerId = 0;
            if (dealer != null) dealerId = dealer.Id;

            var branch = context.DealerBranches
                .Where(e => e.DealerBranchCode.Equals(salesman.DealerBranchCode))
                .FirstOrDefault();

            var branchId = 0;
            var cityId = 0;
            if (branch != null) {
                branchId = branch.Id;
                cityId = branch.CityId;
            }

            return new AnalyticData {
                CityId = cityId,
                JobPositionId = jobPositionId,
                DealerId = dealerId,
                DealerBranchId = branchId
            };
        }

        public static AnalyticData ResolveUserAnalyticData(ISFDDBContext context, Domain.Entities.User source)
        {
            // job position
            var salesman = source.Salesman;
            var jobPosition = context.JobPositions
                .Where(e => e.Description.Equals(salesman.JobDescription))
                .FirstOrDefault();
            var jobPositionId = 0;
            if (jobPosition != null) jobPositionId = jobPosition.Id;

            var dealer = context.Dealers
                .Where(e => e.DealerCode.Equals(salesman.DealerCode))
                .FirstOrDefault();
            var dealerId = 0;
            if (dealer != null) dealerId = dealer.Id;

            var branch = context.DealerBranches
                .Where(e => e.DealerBranchCode.Equals(salesman.DealerBranchCode))
                .FirstOrDefault();

            var branchId = 0;
            var cityId = dealer.CityId;
            if (branch != null) {
                branchId = branch.Id;
                if (branch.CityId > 0) {
                  cityId = branch.CityId;  
                }
            }

            return new AnalyticData {
                CityId = cityId,
                JobPositionId = jobPositionId,
                DealerId = dealerId,
                DealerBranchId = branchId
            };
        }
    }

    public class ProfileData
    {
        public string SalesCode { set; get; }
        public string SalesmanPhone { set; get; }
        public string SalesmanEmail { set; get; }
        public string Name { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string Position { set; get; }
        public string Level { set; get; }
        public string Photo { set; get; }
    }

    public class GradeData
    {
        public string CurrentYear { set; get; }
        public string LastYear { set; get; }
    }

    public class TeamData
    {
        public string Category { set; get; }
        public string EntryDate { set; get; }
        public string Supervisor { set; get; }
    }

    public class DealerData
    {
        public string Name { set; get; }
        public string City { set; get; }
        public string Branch { set; get; }
        public string Group { set; get; }
        public string Code { set; get; }
    }

    public class AnalyticData
    {
        public int DealerId { set; get; }
        public int DealerBranchId { set; get; }
        public int JobPositionId { set; get; }
        public int CityId { set; get; }
    }
}
