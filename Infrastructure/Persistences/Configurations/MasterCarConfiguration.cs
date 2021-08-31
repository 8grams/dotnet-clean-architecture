using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFIDWebAPI.Domain.Entities;

namespace SFIDWebAPI.Infrastructure.Persistences.Configurations
{
    public class MasterCarConfiguration : IEntityTypeConfiguration<MasterCar>
    {
        public void Configure(EntityTypeBuilder<MasterCar> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasMany(d => d.GuideMaterials)
                .WithOne(p => p.MasterCar)
                .HasForeignKey(f => f.MasterCarId);

            builder.HasOne(e => e.ImageCover)
                .WithMany(m => m.MasterCarImageCovers)
                .HasForeignKey(e => e.ImageCoverId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.ImageThumbnail)
                .WithMany(m => m.MasterCarImageThumbnails)
                .HasForeignKey(e => e.ImageThumbnailId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.ImageLogo)
                .WithMany(m => m.MasterCarImageLogos)
                .HasForeignKey(e => e.ImageLogoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.Title)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.Tag)
                .HasColumnName("Tag")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.ImageThumbnailId)
                .HasColumnName("ImageThumbnailId")
                .IsRequired();

            builder.Property(e => e.ImageCoverId)
                .HasColumnName("ImageCoverId")
                .IsRequired();

            builder.Property(e => e.ImageLogoId)
                .HasColumnName("ImageLogoId")
                .IsRequired();

            builder.Property(e => e.TrainingActive)
                .HasColumnType("bit")
                .HasColumnName("TrainingActive")
                .IsRequired();

            builder.Property(e => e.GuideActive)
                .HasColumnType("bit")
                .HasColumnName("GuideActive")
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
            builder.ToTable("SFDMasterCar");
        }
    }
}
