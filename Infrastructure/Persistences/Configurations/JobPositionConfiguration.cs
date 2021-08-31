using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFIDWebAPI.Domain.Entities;

namespace SFIDWebAPI.Infrastructure.Persistences.Configurations
{
    public class JobPositionConfiguration : IEntityTypeConfiguration<JobPosition>
    {
        public void Configure(EntityTypeBuilder<JobPosition> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("ID");

            builder.Property(e => e.Code)
                .HasColumnName("Code")
                .HasMaxLength(100);

            builder.Property(e => e.Description)
                .HasColumnName("Description")
                .HasMaxLength(225);

            builder.Property(e => e.Category)
                .HasColumnName("Category")
                .HasColumnType("tinyint");

            builder.Property(e => e.SalesTarget)
                .HasColumnName("SalesTarget");

            builder.Property(e => e.CreateBy)
                .HasColumnName("CreatedBy")
                .HasMaxLength(50);

            builder.Property(e => e.CreateDate)
                .HasColumnName("CreatedTime")
                .HasColumnType("datetime");

            builder.Property(e => e.LastUpdateBy)
                .HasColumnName("LastUpdateBy")
                .HasMaxLength(50);

            builder.Property(e => e.LastUpdateDate)
                .HasColumnName("LastUpdateTime")
                .HasColumnType("datetime");

            builder.Property(e => e.RowStatus)
                .HasColumnName("RowStatus");

            builder.HasQueryFilter(m => m.RowStatus == 0);
            builder.ToTable("JobPosition");
        }
    }
}
