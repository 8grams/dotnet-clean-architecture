using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFIDWebAPI.Domain.Entities;

namespace SFIDWebAPI.Infrastructure.Persistences.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasMany(a => a.AccessTokens)
                .WithOne(t => t.User)
                .HasForeignKey(a => a.UserId);

            builder.HasMany(a => a.OTPs)
                .WithOne(t => t.User)
                .HasForeignKey(a => a.UserId);

            builder.HasOne(a => a.Dealer)
                .WithMany(e => e.Users)
                .HasForeignKey(a => a.DealerId);

            builder.Property(e => e.DealerId)
                .HasColumnType("smallint")
                .HasColumnName("DealerId");

            builder.HasOne(a => a.MasterConfig)
                .WithMany(t => t.Users)
                .HasForeignKey(a => a.MasterConfigId);

            builder.HasOne(a => a.SalesmanData)
                .WithOne(t => t.User)
                .HasForeignKey<User>(u => u.UserName);

            builder.HasOne(a => a.SalesmanMeta)
                .WithOne(t => t.User)
                .HasForeignKey<User>(u => u.UserName);

            builder.Property(e => e.Email)
                .HasColumnName("Email")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.Phone)
                .HasColumnName("Phone")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.UserName)
                .HasColumnName("Username")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.Password)
                .HasColumnName("Password")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.IsActive)
                .HasColumnName("IsActive")
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(e => e.DeviceId)
                .HasColumnName("DeviceID")
                .IsRequired();

            builder.Property(e => e.LoginThrottle)
                .HasColumnName("LoginThrottle")
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(e => e.LastLogin)
                .HasColumnName("LastLogin")
                .HasColumnType("datetime");

            builder.Property(e => e.ProfilePhoto)
                .HasColumnName("ProfilePhoto")
                .HasMaxLength(500);

            builder.Property(e => e.FirebaseToken)
                .HasColumnName("FirebaseToken")
                .HasMaxLength(500);

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

            builder.Ignore(e => e.RawPassword);
            builder.Ignore(e => e.IsRegistered);
            builder.Ignore(e => e.Salesman);

            builder.ToTable("SFDUser");
        }
    }
}
