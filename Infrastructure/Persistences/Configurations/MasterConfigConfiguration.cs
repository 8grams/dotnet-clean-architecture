using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFIDWebAPI.Domain.Entities;

namespace SFIDWebAPI.Infrastructure.Persistences.Configurations
{
    public class MasterConfigConfiguration : IEntityTypeConfiguration<MasterConfig>
    {
        public void Configure(EntityTypeBuilder<MasterConfig> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasMany(e => e.Roles)
                .WithOne(r => r.MasterConfig)
                .HasForeignKey(r => r.MasterConfigId);

            builder.HasMany(e => e.Users)
                .WithOne(r => r.MasterConfig)
                .HasForeignKey(r => r.MasterConfigId);

            builder.Property(e => e.Category)
                .HasColumnName("Category")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.ValueId)
                .HasColumnName("ValueId")
                .IsRequired();

            builder.Property(e => e.ValueCode)
                .HasColumnName("ValueCode")
                .IsRequired();

            builder.Property(e => e.ValueDesc)
                .HasColumnName("ValueDesc")
                .IsRequired();

            builder.Property(e => e.Sequence)
                .HasColumnName("Sequence")
                .IsRequired();

            builder.Property(e => e.CreateBy)
                .HasColumnName("CreateBy")
                .HasMaxLength(50);

            builder.Property(e => e.CreateDate)
                .HasColumnName("CreateDate")
                .HasColumnType("datetime");

            builder.Property(e => e.LastUpdateBy)
                .HasColumnName("LastUpdateBy")
                .HasMaxLength(50);

            builder.Property(e => e.LastUpdateDate)
                .HasColumnName("LastUpdateDate")
                .HasColumnType("datetime");

            builder.Property(e => e.RowStatus)
                .HasColumnName("RowStatus");

            builder.HasQueryFilter(m => m.RowStatus == 0);
            builder.ToTable("SFDMasterConfig");
        }
    }
}
