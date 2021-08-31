using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFIDWebAPI.Domain.Entities;
using SFIDWebAPI.Application.Misc;

namespace SFIDWebAPI.Infrastructure.Persistences.Configurations
{
    public class SalesmanConfiguration : IEntityTypeConfiguration<Salesman>
    {
        public void Configure(EntityTypeBuilder<Salesman> builder)
        {
            builder.Property(e => e.DealerCode)
                .HasMaxLength(50)
                .HasColumnName("DealerCode");

            builder.Property(e => e.DealerName)
                .HasMaxLength(50)
                .HasColumnName("DealerName");
                
            builder.Property(e => e.DealerCity)
                .HasMaxLength(50)
                .HasColumnName("DealerCity");

            builder.Property(e => e.DealerGroup)
                .HasMaxLength(50)
                .HasColumnName("DealerGroup");

            builder.Property(e => e.DealerArea)
                .HasMaxLength(50)
                .HasColumnName("DealerArea");

            builder.Property(e => e.DealerBranchCode)
                .HasMaxLength(50)
                .HasColumnName("DealerBranchCode");

            builder.Property(e => e.DealerBranchName)
                .HasMaxLength(50)
                .HasColumnName("DealerBranchName");

            builder.Property(e => e.SalesmanCode)
                .HasMaxLength(50)
                .HasColumnName("SalesmanCode");

            builder.Property(e => e.SalesmanName)
                .HasMaxLength(50)
                .HasColumnName("SalesmanName");

            builder.Property(e => e.SalesmanHireDate)
                .HasColumnType("datetime")
                .HasColumnName("SalesmanHireDate");

            builder.Property(e => e.JobDescription)
                .HasMaxLength(50)
                .HasColumnName("JobDescription");

            builder.Property(e => e.LevelDescription)
                .HasMaxLength(100)
                .HasColumnName("LevelDescription");

            builder.Property(e => e.SuperiorName)
                .HasMaxLength(50)
                .HasColumnName("SuperiorName");

            builder.Property(e => e.SuperiorCode)
                .HasMaxLength(50)
                .HasColumnName("SuperiorCode");

            builder.Property(e => e.SalesmanEmail)
                .HasMaxLength(50)
                .HasColumnName("SalesmanEmail");

            builder.Property(e => e.SalesmanHandphone)
                .HasMaxLength(50)
                .HasColumnName("SalesmanHandphone");

            builder.Property(e => e.SalesmanTeamCategory)
                .HasMaxLength(50)
                .HasColumnName("SalesmanTeamCategory");

            builder.Property(e => e.SalesmanStatus)
                .HasMaxLength(50)
                .HasColumnName("SalesmanStatus");

            builder.Property(e => e.LastUpdateTime)
                .HasColumnType("datetime")
                .HasColumnName("LastUpdateTime");

            builder.HasMany(d => d.SalesmanGrades)
                .WithOne(p => p.Salesman)
                .HasForeignKey(f => f.SalesmanHeaderID)
                .HasPrincipalKey(d => d.Id);

            builder.HasKey(e => e.SalesmanCode);

            if (Utils.IsDevelopment())
            {
                builder.ToTable("VWI_SFDMobileSalesman");
            }
            else
            {
                builder.ToView("VWI_SFDMobileSalesman");
                return;
            }
        }
    }
}
