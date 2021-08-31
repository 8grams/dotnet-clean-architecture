using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFIDWebAPI.Domain.Entities;

namespace SFIDWebAPI.Infrastructure.Persistences.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(d => d.MasterConfig)
                .WithMany(e => e.Roles)
                .HasForeignKey(e => e.MasterConfigId);

            builder.HasMany(d => d.Admins)
                .WithOne(e => e.Role)
                .HasForeignKey(d => d.RoleId);

            builder.Property(e => e.Name).HasMaxLength(50).IsRequired();
            builder.Property(e => e.Description).HasMaxLength(50).IsRequired();
            builder.Property(e => e.MasterConfigId).HasColumnName("SFDMCID");
            builder.Property(e => e.CreateBy).HasMaxLength(50);
            builder.Property(e => e.CreateDate).HasColumnType("datetime");
            builder.Property(e => e.LastUpdateBy).HasMaxLength(50);
            builder.Property(e => e.LastUpdateDate).HasColumnType("datetime");
            builder.Property(e => e.RowStatus);
            builder.HasQueryFilter(m => m.RowStatus == 0);

            builder.ToTable("SFDRole");
        }
    }
}
