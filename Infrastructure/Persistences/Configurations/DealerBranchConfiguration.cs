using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFIDWebAPI.Domain.Entities;

namespace SFIDWebAPI.Infrastructure.Persistences.Configurations
{
    public class DealerBranchConfiguration : IEntityTypeConfiguration<DealerBranch>
    {
        public void Configure(EntityTypeBuilder<DealerBranch> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Dealer)
                .WithMany(e => e.DealerBranches)
                .HasForeignKey(e => e.DealerId);
            
            builder.HasOne(e => e.City)
                .WithMany(e => e.DealerBranches)
                .HasForeignKey(e => e.CityId);

            builder.Property(e => e.Id)
                .HasColumnName("ID");

            builder.Property(e => e.DealerId)
                .HasColumnName("DealerID")
                .HasColumnType("smallint");

            builder.Property(e => e.Name)
                .HasColumnName("Name")
                .HasMaxLength(50);

            builder.Property(e => e.Status)
                .HasColumnName("Status")
                .HasMaxLength(1);

            builder.Property(e => e.Address)
                .HasColumnName("Address")
                .HasMaxLength(100);

            builder.Property(e => e.CityId)
                .HasColumnName("CityID")
                .HasColumnType("smallint");

            builder.Property(e => e.ZipCode)
                .HasColumnName("ZipCode")
                .HasMaxLength(50);

            builder.Property(e => e.ProvinceId)
                .HasColumnName("ProvinceID");

            builder.Property(e => e.Phone)
                .HasColumnName("Phone")
                .HasMaxLength(50);

            builder.Property(e => e.Fax)
                .HasColumnName("Fax")
                .HasMaxLength(20);

            builder.Property(e => e.Website)
                .HasColumnName("Website")
                .HasMaxLength(20);

            builder.Property(e => e.Email)
                .HasColumnName("Email")
                .HasMaxLength(40);

            builder.Property(e => e.TypeBranch)
                .HasColumnName("TypeBranch")
                .HasMaxLength(5);

            builder.Property(e => e.DealerBranchCode)
                .HasColumnName("DealerBranchCode")
                .HasMaxLength(50);

            builder.Property(e => e.Term1)
                .HasColumnName("Term1")
                .HasMaxLength(100);

            builder.Property(e => e.Term2)
                .HasColumnName("Term2")
                .HasMaxLength(100);

            builder.Property(e => e.MainAreaId)
                .HasColumnName("MainAreaId");    

            builder.Property(e => e.Area1Id)
                .HasColumnName("Area1ID");

            builder.Property(e => e.Area2Id)
                .HasColumnName("Area2ID");

            builder.Property(e => e.BranchAssignmentNo)
                .HasColumnName("BranchAssignmentNo")
                .HasMaxLength(50);

            builder.Property(e => e.BranchAssignmentDate)
                .HasColumnName("BranchAssignmentDate")
                .HasColumnType("datetime");

            builder.Property(e => e.SalesUnitFlag)
                .HasColumnName("SalesUnitFlag")
                .HasMaxLength(1);

            builder.Property(e => e.ServiceFlag)
                .HasColumnName("ServiceFlag")
                .HasMaxLength(1);

            builder.Property(e => e.SparePartFlag)
                .HasColumnName("SparePartFlag")
                .HasMaxLength(1);

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
        }
    }
}
