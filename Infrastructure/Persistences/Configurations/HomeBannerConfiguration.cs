using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFIDWebAPI.Domain.Entities;

namespace SFIDWebAPI.Infrastructure.Persistences.Configurations
{
    public class HomeBannerConfiguration : IEntityTypeConfiguration<HomeBanner>
    {
        public void Configure(EntityTypeBuilder<HomeBanner> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .HasColumnName("Name")
                .HasMaxLength(50)
                .IsRequired();

            builder.HasOne(e => e.Image)
                .WithMany(m => m.HomeBannerImages)
                .HasForeignKey(e => e.ImageId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.ImageId)
                .HasColumnName("ImageId")
                .IsRequired();

            builder.Property(e => e.PublishedAt)
                .HasColumnName("PublishedAt")
                .HasColumnType("datetime");

            builder.Property(e => e.ExpiresAt)
                .HasColumnName("ExpiresAt")
                .HasColumnType("datetime");

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
            builder.ToTable("SFDHomeBanner");
        }
    }
}
