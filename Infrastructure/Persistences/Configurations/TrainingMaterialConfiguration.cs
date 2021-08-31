using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFIDWebAPI.Domain.Entities;

namespace SFIDWebAPI.Infrastructure.Persistences.Configurations
{
    public class TrainingMaterialConfiguration : IEntityTypeConfiguration<TrainingMaterial>
    {
        public void Configure(EntityTypeBuilder<TrainingMaterial> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(d => d.MasterCar)
                .WithMany(c => c.TrainingMaterials)
                .HasForeignKey(d => d.MasterCarId);

            builder.HasOne(e => e.ImageThumbnail)
                .WithMany(m => m.TrainingMaterialImageThumbnails)
                .HasForeignKey(e => e.ImageThumbnailId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.MasterCarId)
                .HasColumnName("SFDMasterCarId")
                .IsRequired();

            builder.Property(e => e.Title)
                .HasColumnName("Title")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.ImageThumbnailId)
                .HasColumnName("ImageThumbnailId")
                .IsRequired();

            builder.Property(e => e.Link)
                .HasColumnName("Link")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(e => e.FileType)
                .HasColumnName("FileType")
                .HasMaxLength(50);

            builder.Property(e => e.FileCode)
                .HasColumnName("FileCode")
                .HasMaxLength(50);

            builder.Property(e => e.TotalViews)
                .HasColumnName("TotalViews");

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

            builder.Property(e => e.PublishedAt)
                .HasColumnName("PublishedAt")
                .HasColumnType("datetime");

            builder.Property(e => e.ExpiresAt)
                .HasColumnName("ExpiresAt")
                .HasColumnType("datetime");

            builder.HasQueryFilter(m => m.RowStatus == 0);
            builder.ToTable("SFDTrainingMaterial");
        }
    }
}
