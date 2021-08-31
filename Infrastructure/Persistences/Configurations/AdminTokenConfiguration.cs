using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFIDWebAPI.Domain.Entities;

namespace SFIDWebAPI.Infrastructure.Persistences.Configurations
{
    public class AdminTokenConfiguration : IEntityTypeConfiguration<AdminToken>
    {
        public void Configure(EntityTypeBuilder<AdminToken> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(d => d.Admin)
                .WithMany(c => c.AdminTokens)
                .HasForeignKey(d => d.AdminId);

            builder.Property(e => e.AuthToken)
                .HasColumnName("AuthToken")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(e => e.Type)
                .HasColumnName("Type")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.AdminId)
                .HasColumnName("AdminId")
                .IsRequired();

            builder.Property(e => e.ExpiresAt)
                .HasColumnName("ExpiresAt")
                .HasMaxLength(50);

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
            builder.ToTable("SFDAdminToken");
        }
    }
}
