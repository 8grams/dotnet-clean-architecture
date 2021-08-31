using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFIDWebAPI.Domain.Entities;

namespace SFIDWebAPI.Infrastructure.Persistences.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title)
                .HasColumnName("Title")
                .HasMaxLength(50)
                .IsRequired();

            builder.HasOne(a => a.NotificationStatus)
                .WithOne(t => t.Notification)
                .HasForeignKey<NotificationStatus>(u => u.NotificationId);

            builder.Property(e => e.OwnerId)
                .HasColumnName("OwnerId")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.OwnerType)
                .HasColumnName("OwnerType")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.Content)
                .HasColumnName("Content")
                .IsRequired();

            builder.Property(e => e.Attachment)
                .HasColumnName("Attachment")
                .HasMaxLength(500);

            builder.Property(e => e.IsDeletable)
                .HasColumnName("IsDeletable")
                .HasColumnType("bit")
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
            builder.ToTable("SFDNotification");

        }
    }
}
