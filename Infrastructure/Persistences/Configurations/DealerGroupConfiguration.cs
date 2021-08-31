using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFIDWebAPI.Domain.Entities;

namespace SFIDWebAPI.Infrastructure.Persistences.Configurations
{
    public class DealerGroupConfiguration : IEntityTypeConfiguration<DealerGroup>
    {
        public void Configure(EntityTypeBuilder<DealerGroup> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("ID");

            builder.HasMany(e => e.Dealers)
                .WithOne(e => e.DealerGroup)
                .HasForeignKey(e => e.DealerGroupId);

            builder.Property(e => e.DealerGroupCode)
                .HasColumnName("DealerGroupCode")
                .HasMaxLength(10);

            builder.Property(e => e.GroupName)
                .HasColumnName("GroupName")
                .HasMaxLength(10);

            builder.Property(e => e.CreateBy)
                .HasColumnName("CreatedBy")
                .HasMaxLength(50);

            builder.Property(e => e.CreateDate)
                .HasColumnName("CreatedTime")
                .HasColumnType("datetime");

            builder.Property(e => e.LastUpdateBy)
                .HasColumnName("LastUpdateBy")
                .HasMaxLength(50);

            builder.Property(e => e.LastUpdateDate)
                .HasColumnName("LastUpdateTime")
                .HasColumnType("datetime");

            builder.Property(e => e.RowStatus)
                .HasColumnName("RowStatus");

            builder.HasQueryFilter(m => m.RowStatus == 0);
            builder.ToTable("DealerGroup");
        }
    }
}
