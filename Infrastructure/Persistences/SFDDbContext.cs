using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Domain.Entities;
using SFIDWebAPI.Domain.Infrastructures;
using StoredProcedureEFCore;

namespace SFIDWebAPI.Infrastructure.Persistences
{
    public class SFDDbContext : DbContext, ISFDDBContext
    {
        public SFDDbContext(DbContextOptions<SFDDbContext> options) : base(options)
        {

        }

        public DbSet<AccessToken> AccessTokens { set; get; }
        public DbSet<Bulletin> Bulletins { set; get; }
        public DbSet<MasterCar> MasterCars { set; get; }
        public DbSet<TrainingMaterial> TrainingMaterials { set; get; }
        public DbSet<GuideMaterial> GuideMaterials { set; get; }
        public DbSet<Faq> Faqs { set; get; }
        public DbSet<MasterConfig> MasterConfigs { set; get; }
        public DbSet<Menu> Menus { set; get; }
        public DbSet<MenuType> MenuTypes { set; get; }
        public DbSet<HomeBanner> HomeBanners { set; get; }
        public DbSet<Notification> Notifications { set; get; }
        public DbSet<NotificationStatus> NotificationStatuses { set; get; }
        public DbSet<MaterialStatus> MaterialStatuses { set; get; }
        public DbSet<Permission> Permissions { set; get; }
        public DbSet<Role> Roles { set; get; }
        public DbSet<User> Users { set; get; }
        public DbSet<Salesman> Salesmen { set; get; }
        public DbSet<SalesmanMeta> SalesmenMeta { set; get; }
        public DbSet<Recommendation> Recommendations { set; get; }
        public DbSet<AdditionalInfo> AdditionalInfos { set; get; }
        public DbSet<OTP> OTPs { set; get; }
        public DbSet<StaticContent> StaticContent { set; get; }
        public DbSet<PKTHistory> PKTHistories { set; get; }
        public DbSet<Admin> Admins { set; get; }
        public DbSet<AdminToken> AdminTokens { set; get; }
        public DbSet<ImageGallery> ImageGalleries { set; get; }
        public DbSet<Dealer> Dealers { set; get; }
        public DbSet<DealerBranch> DealerBranches { set; get; }
        public DbSet<DealerGroup> DealerGroups { set; get; }
        public DbSet<City> Cities { set; get; }
        public DbSet<SalesmanLevel> SalesmanLevels { set; get; }
        public DbSet<SalesmanGrade> SalesmanGrades { set; get; }
        public DbSet<JobPosition> JobPositions { set; get; }
        public DbSet<PositionMeta> PositionMetas { set; get; }
        public DbSet<MaterialCounter> MaterialCounters { set; get; }
        public DbSet<UserPresence> UserPresences { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SFDDbContext).Assembly);
        }

        public override EntityEntry Add(object entity)
        {
            if (entity is BaseEntity)
            {
                ((BaseEntity)entity).RowStatus = 0;
            }

            return base.Add(entity);
        }

        public IStoredProcBuilder loadStoredProcedureBuilder(string val)
        {
            return this.LoadStoredProc(val);
        }

        public override int SaveChanges()
        {
            this.HandleSave();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            this.HandleSave();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.HandleSave();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void HandleSave()
        {
            var entries = ChangeTracker.Entries().Where(e => e.Entity is BaseEntity && (
                e.State == EntityState.Added
                || e.State == EntityState.Modified || e.State == EntityState.Deleted));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).LastUpdateDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreateDate = DateTime.Now;
                    ((BaseEntity)entityEntry.Entity).RowStatus = 0;
                }

                if (!(entityEntry.Entity is User) && !(entityEntry.Entity is Recommendation) && !(entityEntry.Entity is SalesmanMeta))
                {
                    UpdateSoftDeleteStatuses(entityEntry);
                }
            }
        }

        private void UpdateSoftDeleteStatuses(EntityEntry entry)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    ((BaseEntity)entry.Entity).RowStatus = 0;
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    ((BaseEntity)entry.Entity).RowStatus = -1;
                    break;
            }
        }
    }
}
