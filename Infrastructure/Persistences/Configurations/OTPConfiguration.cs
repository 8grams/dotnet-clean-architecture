using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFIDWebAPI.Domain.Entities;

namespace SFIDWebAPI.Infrastructure.Persistences.Configurations
{
    public class OTPConfiguration : IEntityTypeConfiguration<OTP>
    {
        public void Configure(EntityTypeBuilder<OTP> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(d => d.User)
                .WithMany(r => r.OTPs)
                .HasForeignKey(d => d.UserId);

            builder.Property(e => e.Pin)
                .HasColumnName("PIN")
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(e => e.UserId)
                .HasColumnName("UserId")
                .IsRequired();

            builder.Property(e => e.ExpiresAt)
                .HasColumnName("ExpiresAt")
                .HasColumnType("datetime");

            builder.ToTable("SFDOTP");
        }
    }
}
