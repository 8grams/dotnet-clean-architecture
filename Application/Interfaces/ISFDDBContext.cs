using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using StoredProcedureEFCore;

namespace SFIDWebAPI.Application.Interfaces
{
    public interface ISFDDBContext
    {
        DbSet<AccessToken> AccessTokens { set; get; }
        DbSet<Bulletin> Bulletins { set; get; }
        DbSet<MasterCar> MasterCars { set; get; }
        DbSet<TrainingMaterial> TrainingMaterials { set; get; }
        DbSet<GuideMaterial> GuideMaterials { set; get; }
        DbSet<Faq> Faqs { set; get; }
        DbSet<MasterConfig> MasterConfigs { set; get; }
        DbSet<Menu> Menus { set; get; }
        DbSet<MenuType> MenuTypes { set; get; }
        DbSet<HomeBanner> HomeBanners { set; get; }
        DbSet<Notification> Notifications { set; get; }
        DbSet<NotificationStatus> NotificationStatuses { set; get; }
        DbSet<MaterialStatus> MaterialStatuses { set; get; }
        DbSet<Permission> Permissions { set; get; }

        DbSet<Role> Roles { set; get; }
        DbSet<User> Users { set; get; }
        DbSet<Salesman> Salesmen { set; get; }
        DbSet<SalesmanMeta> SalesmenMeta { set; get; }
        DbSet<Recommendation> Recommendations { set; get; }
        DbSet<AdditionalInfo> AdditionalInfos { set; get; }
        DbSet<OTP> OTPs { set; get; }
        DbSet<StaticContent> StaticContent { set; get; }
        DbSet<PKTHistory> PKTHistories { set; get; }
        DbSet<Admin> Admins { set; get; }
        DbSet<AdminToken> AdminTokens { set; get; }
        DbSet<ImageGallery> ImageGalleries { set; get; }
        DbSet<Dealer> Dealers { set; get; }
        DbSet<DealerBranch> DealerBranches { set; get; }
        DbSet<DealerGroup> DealerGroups { set; get; }
        DbSet<City> Cities { set; get; }
        DbSet<JobPosition> JobPositions { set; get; }
        DbSet<PositionMeta> PositionMetas { set; get; }
        DbSet<SalesmanLevel> SalesmanLevels { set; get; }
        DbSet<SalesmanGrade> SalesmanGrades { set; get; }
        DbSet<MaterialCounter> MaterialCounters { set; get; }
        DbSet<UserPresence> UserPresences { set; get; }

        IStoredProcBuilder loadStoredProcedureBuilder(string val);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
