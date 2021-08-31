using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFIDWebAPI.Domain.Entities;

namespace SFIDWebAPI.Infrastructure.Persistences.Configurations
{
    public class NotificationStatusConfiguration : IEntityTypeConfiguration<NotificationStatus>
    {
        public void Configure(EntityTypeBuilder<NotificationStatus> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.UserId)
                .HasColumnName("UserId")
                .IsRequired();

            builder.Property(e => e.NotificationId)
                .HasColumnName("NotificationId")
                .IsRequired();

            builder.Property(e => e.IsDeleted)
                .HasColumnName("IsDeleted")
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(e => e.HasRead)
                .HasColumnName("HasRead")
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
            builder.ToTable("SFDNotificationStatus");
        }
    }
}
