using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFIDWebAPI.Domain.Entities;

namespace SFIDWebAPI.Infrastructure.Persistences.Configurations
{
    public class AccessTokenConfiguration : IEntityTypeConfiguration<AccessToken>
    {
        public void Configure(EntityTypeBuilder<AccessToken> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(u => u.User)
                .WithMany(a => a.AccessTokens)
                .HasForeignKey(a => a.UserId);

            builder.Property(a => a.AuthToken)
                .HasColumnName("AuthToken")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.Type)
                .HasColumnName("Type")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(a => a.UserId)
                .HasColumnName("UserId")
                .IsRequired();

            builder.Property(a => a.ExpiresAt)
                .HasColumnName("ExpiresAt")
                .HasColumnType("datetime")
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
            builder.ToTable("SFDAccessToken");
        }
    }
}
