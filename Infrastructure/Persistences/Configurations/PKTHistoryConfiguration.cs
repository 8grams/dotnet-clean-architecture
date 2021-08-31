using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFIDWebAPI.Domain.Entities;

namespace SFIDWebAPI.Infrastructure.Persistences.Configurations
{
    public class PKTHistoryConfiguration : IEntityTypeConfiguration<PKTHistory>
    {
        public void Configure(EntityTypeBuilder<PKTHistory> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Salesman)
                .WithMany(m => m.PKTHistories)
                .HasForeignKey(e => e.SalesCode)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.Vin)
                .HasColumnName("Vin")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.Image)
                .HasColumnName("Image")
                .HasMaxLength(100);

            builder.Property(e => e.PDIDate)
                .HasColumnName("PDIDate")
                .HasColumnType("datetime");

            builder.Property(e => e.Status)
                .HasColumnName("Status");

            builder.Property(e => e.ReportType)
                .HasColumnName("ReportType")
                .HasMaxLength(100)
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


            builder.HasQueryFilter(m => m.RowStatus == 0);
            builder.ToTable("SFDPKTHistory");
        }
    }
}
