using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFIDWebAPI.Domain.Entities;

namespace SFIDWebAPI.Infrastructure.Persistences.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(d => d.Role)
                .WithMany(p => p.Permissions)
                .HasForeignKey(d => d.RoleId);

            builder.HasOne(d => d.Menu)
                .WithMany(r => r.Permissions)
                .HasForeignKey(d => d.MenuId);

            builder.Property(e => e.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.Description)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.RoleId).HasColumnName("SFDRoleID");
            builder.Property(e => e.MenuId).HasColumnName("SFDMenuID");

            builder.Property(e => e.CreateBy).HasMaxLength(50);
            builder.Property(e => e.CreateDate).HasColumnType("datetime");
            builder.Property(e => e.LastUpdateBy).HasMaxLength(50);
            builder.Property(e => e.LastUpdateDate).HasColumnType("datetime");
            builder.Property(e => e.RowStatus);
            builder.HasQueryFilter(m => m.RowStatus == 0);

            builder.ToTable("SFDPermission");
        }
    }
}
