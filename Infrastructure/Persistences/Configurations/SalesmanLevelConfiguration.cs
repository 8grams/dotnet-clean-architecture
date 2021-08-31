using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFIDWebAPI.Domain.Entities;
using SFIDWebAPI.Application.Misc;

namespace SFIDWebAPI.Infrastructure.Persistences.Configurations
{
    public class SalesmanLevelConfiguration : IEntityTypeConfiguration<SalesmanLevel>
    {
        public void Configure(EntityTypeBuilder<SalesmanLevel> builder)
        {
            builder.HasKey(e => e.Id);
            
            builder.Property(e => e.Id)
                .HasColumnName("ID");

            builder.Property(e => e.Description)
                .HasColumnName("Description")
                .HasMaxLength(30);
                
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
            builder.ToTable("DealerBranch");
                
            builder.ToTable("SalesmanLevel");
        }
    }
}
