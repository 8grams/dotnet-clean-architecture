using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFIDWebAPI.Domain.Entities;
using SFIDWebAPI.Application.Misc;

namespace SFIDWebAPI.Infrastructure.Persistences.Configurations
{
    public class SalesmanGradeConfiguration : IEntityTypeConfiguration<SalesmanGrade>
    {
        public void Configure(EntityTypeBuilder<SalesmanGrade> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(d => d.Salesman)
                .WithMany(c => c.SalesmanGrades)
                .HasForeignKey(d => d.SalesmanHeaderID)
                .HasPrincipalKey(d => d.Id);

            builder.Property(e => e.Id)
                .HasColumnName("ID");

            builder.Property(e => e.SalesmanHeaderID)
                .HasColumnName("SalesmanHeaderID")
                .HasColumnType("smallint");

            builder.Property(e => e.Year)
                .HasColumnName("Year")
                .HasColumnType("smallint");

            builder.Property(e => e.Period)
                .HasColumnName("Period")
                .HasColumnType("smallint");

            builder.Property(e => e.Grade)
                .HasColumnName("Grade")
                .HasColumnType("smallint");

            builder.Property(e => e.Status)
                .HasColumnName("Status")
                .HasColumnType("smallint");

            builder.Property(e => e.RowStatus)
                .HasColumnName("RowStatus");

            builder.HasQueryFilter(m => m.RowStatus == 0);

            builder.Property(e => e.CreateBy)
               .HasColumnName("CreatedBy")
               .HasColumnType("nvarchar(20)")
               .HasMaxLength(20);

            builder.Property(e => e.CreateDate)
                .HasColumnName("CreatedTime")
                .HasColumnType("datetime");

            builder.Property(e => e.LastUpdateBy)
               .HasColumnName("LastUpdatedBy")
               .HasColumnType("nvarchar(20)")
               .HasMaxLength(20);

            builder.Property(e => e.LastUpdateDate)
               .HasColumnName("LastUpdateTime")
               .HasColumnType("datetime");


            builder.ToTable("SalesmanGrade");
        }
    }
}
