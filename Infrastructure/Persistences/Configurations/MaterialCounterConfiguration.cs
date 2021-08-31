using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFIDWebAPI.Domain.Entities;

namespace SFIDWebAPI.Infrastructure.Persistences.Configurations
{
    public class MaterialCounterConfiguration : IEntityTypeConfiguration<MaterialCounter>
    {
        public void Configure(EntityTypeBuilder<MaterialCounter> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.UserId)
                .HasColumnName("UserId")
                .IsRequired();

            builder.Property(e => e.ContentId)
                .HasColumnName("ContentId")
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(e => e.ContentType)
                .HasColumnName("ContentType")
                .HasDefaultValue(0)
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
            builder.ToTable("SFDMaterialCounter");
        }
    }
}
