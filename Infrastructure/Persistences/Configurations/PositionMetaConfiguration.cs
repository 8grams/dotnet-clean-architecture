using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFIDWebAPI.Domain.Entities;

namespace SFIDWebAPI.Infrastructure.Persistences.Configurations
{
    public class PositionMetaConfiguration : IEntityTypeConfiguration<PositionMeta>
    {
        public void Configure(EntityTypeBuilder<PositionMeta> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Code)
                .HasColumnName("Code")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.Description)
                .HasColumnName("Description")
                .HasMaxLength(100)
                .IsRequired();

            builder.HasMany(e => e.SalesmenMeta)
                .WithOne(a => a.PositionMeta)
                .HasForeignKey(e => e.PositionMetaId);

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

            builder.ToTable("SFDPosition");
        }
    }
}
