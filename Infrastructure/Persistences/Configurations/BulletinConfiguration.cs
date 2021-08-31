using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFIDWebAPI.Domain.Entities;

namespace SFIDWebAPI.Infrastructure.Persistences.Configurations
{
    public class BulletinConfiguration : IEntityTypeConfiguration<Bulletin>
    {
        public void Configure(EntityTypeBuilder<Bulletin> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.ImageThumbnail)
                .WithMany(m => m.BulletinInfoImageThumbnails)
                .HasForeignKey(e => e.ImageThumbnailId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.Title)
                .HasColumnName("Title")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.Link)
                .HasColumnName("Link")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(e => e.FileType)
                .HasColumnName("FileType")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.FileCode)
                .HasColumnName("FileCode")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.ImageThumbnailId)
                .HasColumnName("ImageThumbnailId")
                .IsRequired();

            builder.Property(e => e.IsActive)
                .HasColumnName("IsActive")
                .HasColumnType("bit")
                .IsRequired();

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

            builder.Property(e => e.PublishedAt)
                .HasColumnName("PublishedAt")
                .HasColumnType("datetime");

            builder.Property(e => e.ExpiresAt)
                .HasColumnName("ExpiresAt")
                .HasColumnType("datetime");

            builder.Property(e => e.RowStatus)
                .HasColumnName("RowStatus");

            builder.HasQueryFilter(m => m.RowStatus == 0);
            builder.ToTable("SFDBulletin");
        }
    }
}
