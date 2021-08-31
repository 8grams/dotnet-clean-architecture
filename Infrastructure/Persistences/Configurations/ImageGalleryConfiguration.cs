using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFIDWebAPI.Domain.Entities;

namespace SFIDWebAPI.Infrastructure.Persistences.Configurations
{
    public class ImageGalleryConfigConfiguration : IEntityTypeConfiguration<ImageGallery>
    {
        public void Configure(EntityTypeBuilder<ImageGallery> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasMany(e => e.AdditionalInfoImageThumbnails)
                .WithOne(r => r.ImageThumbnail)
                .HasForeignKey(r => r.ImageThumbnailId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.BulletinInfoImageThumbnails)
                .WithOne(r => r.ImageThumbnail)
                .HasForeignKey(r => r.ImageThumbnailId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.MasterCarImageCovers)
                .WithOne(r => r.ImageCover)
                .HasForeignKey(r => r.ImageCoverId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.MasterCarImageThumbnails)
                .WithOne(r => r.ImageThumbnail)
                .HasForeignKey(r => r.ImageThumbnailId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.GuideMaterialImageThumbnails)
                .WithOne(r => r.ImageThumbnail)
                .HasForeignKey(r => r.ImageThumbnailId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.TrainingMaterialImageThumbnails)
                .WithOne(r => r.ImageThumbnail)
                .HasForeignKey(r => r.ImageThumbnailId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.MasterCarImageLogos)
                .WithOne(r => r.ImageLogo)
                .HasForeignKey(r => r.ImageLogoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.HomeBannerImages)
                .WithOne(r => r.Image)
                .HasForeignKey(r => r.ImageId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.Name)
                .HasColumnName("Name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.Link)
                .HasColumnName("Link")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(e => e.Category)
                .HasColumnName("Category")
                .HasMaxLength(100)
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
            builder.ToTable("SFDGuideMaterial");

            builder.ToTable("SFDImageGallery");
        }
    }
}
