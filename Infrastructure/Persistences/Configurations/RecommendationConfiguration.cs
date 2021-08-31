using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFIDWebAPI.Domain.Entities;

namespace SFIDWebAPI.Infrastructure.Persistences.Configurations
{
    public class RecommendationConfiguration : IEntityTypeConfiguration<Recommendation>
    {
        public void Configure(EntityTypeBuilder<Recommendation> builder)
        {
            builder.HasKey(e => new {
                e.ContentId, e.ContentType
            });

            builder.Property(e => e.ContentType)
                .HasColumnName("ContentType")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.ContentId)
                .HasColumnName("ContentId")
                .IsRequired();

            builder.Property(e => e.PublishedAt)
                .HasColumnName("PublishedAt")
                .HasColumnType("datetime");

            builder.Property(e => e.ExpiresAt)
                .HasColumnName("ExpiresAt")
                .HasColumnType("datetime");

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
                
            builder.ToTable("SFDRecommendation");
        }
    }
}
