using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFIDWebAPI.Domain.Entities;

namespace SFIDWebAPI.Infrastructure.Persistences.Configurations
{
    public class UserPresenceConfiguration : IEntityTypeConfiguration<UserPresence>
    {
        public void Configure(EntityTypeBuilder<UserPresence> builder)
        {
            builder.HasKey(e => e.Uuid);

            builder.Property(e => e.DealerId)
                .HasColumnType("smallint")
                .HasColumnName("DealerId");

            builder.Property(e => e.JobPositionId)
                .HasColumnType("smallint")
                .HasColumnName("JobPositionId");

            builder.HasOne(a => a.User)
                .WithMany(e => e.UserPresences)
                .HasForeignKey(a => a.UserId);

            builder.Property(e => e.UserId)
                .HasColumnName("UserId");

            builder.Property(e => e.JobPositionId)
                .HasColumnType("smallint")
                .HasColumnName("JobPositionId");

            builder.Property(e => e.DealerBranchId)
                .HasColumnType("smallint")
                .HasColumnName("DealerBranchId");

            builder.Property(e => e.CreateDate)
                .HasColumnName("CreateDate")
                .HasColumnType("datetime");

            builder.ToTable("SFDUserPresence");
        }
    }
}
