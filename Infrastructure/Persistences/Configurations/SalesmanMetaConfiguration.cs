using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFIDWebAPI.Domain.Entities;

namespace SFIDWebAPI.Infrastructure.Persistences.Configurations
{
    public class SalesmanMetaConfiguration : IEntityTypeConfiguration<SalesmanMeta>
    {
        public void Configure(EntityTypeBuilder<SalesmanMeta> builder)
        {
            builder.HasKey(e => e.SalesmanCode);

            builder.HasOne(e => e.JobPosition)
                .WithMany(d => d.SalesmenMeta)
                .HasForeignKey(e => e.JobPositionId);

            builder.HasOne(e => e.DealerBranch)
                .WithMany(d => d.SalesmenMeta)
                .HasForeignKey(e => e.DealerBranchId);

            builder.HasOne(e => e.SalesmanLevel)
                .WithMany(d => d.SalesmenMeta)
                .HasForeignKey(e => e.SalesmanLevelId);

            builder.HasOne(e => e.PositionMeta)
                .WithMany(d => d.SalesmenMeta)
                .HasForeignKey(e => e.PositionMetaId);

            builder.Property(e => e.DealerBranchId)
                .HasColumnName("DealerBranchId")
                .IsRequired();

            builder.Property(e => e.PositionMetaId)
                .HasColumnName("PositionMetaId");

            builder.Property(e => e.SalesmanCode)
                .HasMaxLength(50)
                .HasColumnName("SalesmanCode");

            builder.Property(e => e.SalesmanName)
                .HasMaxLength(50)
                .HasColumnName("SalesmanName");

            builder.Property(e => e.SalesmanHireDate)
                .HasColumnType("datetime")
                .HasColumnName("SalesmanHireDate");

            builder.Property(e => e.SuperiorName)
                .HasMaxLength(50)
                .HasColumnName("SuperiorName");

            builder.Property(e => e.SalesmanEmail)
                .HasMaxLength(50)
                .HasColumnName("SalesmanEmail");

            builder.Property(e => e.SalesmanHandphone)
                .HasMaxLength(50)
                .HasColumnName("SalesmanHandphone");

            builder.Property(e => e.GradeLastYear)
                .HasColumnName("GradeLastYear")
                .HasColumnType("smallint");

            builder.Property(e => e.GradeCurrentYear)
                .HasColumnName("GradeCurrentYear")
                .HasColumnType("smallint");
            
            builder.ToTable("SFDSalesmanMeta");
        }
    }
}
