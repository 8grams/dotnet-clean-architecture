using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFIDWebAPI.Domain.Entities;

namespace SFIDWebAPI.Infrastructure.Persistences.Configurations
{
    public class MaterialStatusConfiguration : IEntityTypeConfiguration<MaterialStatus>
    {
        public void Configure(EntityTypeBuilder<MaterialStatus> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.UserId)
                .HasColumnName("UserId")
                .IsRequired();

            builder.Property(e => e.NewBulletin)
                .HasColumnName("NewBulletin")
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(e => e.NewInfo)
                .HasColumnName("NewInfo")
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(e => e.NewTraining)
                .HasColumnName("NewTraining")
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(e => e.NewGuide)
                .HasColumnName("NewGuide")
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
            builder.ToTable("SFDMaterialStatus");
        }
    }
}
