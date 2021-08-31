using System;
using System.Collections.Generic;
using SFIDWebAPI.Domain.Infrastructures;

namespace SFIDWebAPI.Domain.Entities
{
    public class User : BaseEntity
    {
        public User()
        {
            AccessTokens = new HashSet<AccessToken>();
            OTPs = new HashSet<OTP>();
            UserPresences = new HashSet<UserPresence>();
        }

        public string Email { set; get; }
        public string Phone { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public bool IsActive { set; get; }
        
        public string DeviceId { set; get; }
        public int MasterConfigId { set; get; }
        public DateTime? LastLogin { set; get; }
        public string RawPassword { set; get; }
        public bool IsRegistered { set; get; }
        public string ProfilePhoto { set; get; }
        public int LoginThrottle { set; get; }

        public string FirebaseToken { set; get; }
        public int? DealerId { set; get; }

        public virtual MasterConfig MasterConfig { set; get; }

        public virtual Salesman SalesmanData { set; get; }
        public virtual Dealer Dealer { set; get; }
        public Salesman Salesman
        { 
            set
            {
                this.SalesmanData = value;
            } 
            get
            {
                if (this.SalesmanData == null && this.SalesmanMeta != null)
                {
                    var currentYear = DateTime.Now.Year;
                    var lastYear = currentYear - 1;

                    List<SalesmanGrade> grades = new List<SalesmanGrade>();
                    grades.Add(new SalesmanGrade() {
                        Year = (Int16) currentYear,
                        Period = 1,
                        Grade = this.SalesmanMeta.GradeCurrentYear,
                        Status = 1
                    });

                    grades.Add(new SalesmanGrade() {
                        Year = (Int16) lastYear,
                        Period = 4,
                        Grade = this.SalesmanMeta.GradeLastYear,
                        Status = 1
                    });

                    this.SalesmanData = new Salesman
                    {
                        DealerCode = this.SalesmanMeta.DealerBranch.Dealer.DealerCode,
                        DealerName = this.SalesmanMeta.DealerBranch.Dealer.DealerName,
                        DealerCity = this.SalesmanMeta.DealerBranch.City.CityName,
                        DealerGroup = this.SalesmanMeta.DealerBranch.Dealer.DealerGroup.GroupName,
                        DealerArea = "",
                        DealerBranchCode = this.SalesmanMeta.DealerBranch.DealerBranchCode,
                        DealerBranchName = this.SalesmanMeta.DealerBranch.Name,
                        SalesmanCode = this.SalesmanMeta.SalesmanCode,
                        SalesmanName = this.SalesmanMeta.SalesmanName,
                        SalesmanHireDate = this.SalesmanMeta.SalesmanHireDate,
                        JobDescription = this.SalesmanMeta.PositionMeta != null ? this.SalesmanMeta.PositionMeta.Description : this.SalesmanMeta.JobPosition.Description,
                        LevelDescription = this.SalesmanMeta.SalesmanLevel.Description,
                        SuperiorName = this.SalesmanMeta.SuperiorName,
                        SuperiorCode = "",
                        SalesmanEmail = this.SalesmanMeta.SalesmanEmail,
                        SalesmanHandphone = this.SalesmanMeta.SalesmanHandphone,
                        SalesmanGrades = grades
                    };
                }

                return this.SalesmanData;
            } 
        }
        public virtual SalesmanMeta SalesmanMeta { set; get; }
        public virtual ICollection<AccessToken> AccessTokens { get; private set; }
        public virtual ICollection<OTP> OTPs { get; private set; }
        public virtual ICollection<UserPresence> UserPresences { get; private set; }
    }
}
