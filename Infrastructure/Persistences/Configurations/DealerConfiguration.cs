using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFIDWebAPI.Domain.Entities;

namespace SFIDWebAPI.Infrastructure.Persistences.Configurations
{
    public class DealerConfiguration : IEntityTypeConfiguration<Dealer>
    {
        public void Configure(EntityTypeBuilder<Dealer> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasMany(e => e.DealerBranches)
                .WithOne(e => e.Dealer)
                .HasForeignKey(e => e.DealerId);

            builder.HasMany(e => e.Users)
                .WithOne(a => a.Dealer)
                .HasForeignKey(e => e.DealerId);
                
            builder.Property(e => e.Id)
                .HasColumnType("smallint")
                .HasColumnName("ID");

            builder.Property(e => e.MainDealerId)
                .HasColumnName("MainDealerID")
                .HasColumnType("smallint");

            builder.Property(e => e.DealerCode)
                .HasColumnName("DealerCode")
                .HasMaxLength(10);

            builder.Property(e => e.DealerName)
                .HasColumnName("DealerName")
                .HasMaxLength(50);

            builder.Property(e => e.Status)
                .HasColumnName("Status")
                .HasMaxLength(1);

            builder.Property(e => e.Title)
                .HasColumnName("Title")
                .HasMaxLength(5);

            builder.Property(e => e.SearchTerm1)
                .HasColumnName("SearchTerm1")
                .HasMaxLength(20);

            builder.Property(e => e.SearchTerm2)
                .HasColumnName("SearchTerm2")
                .HasMaxLength(20);

            builder.Property(e => e.DealerGroupId)
                .HasColumnName("DealerGroupID");

            builder.Property(e => e.Address)
                .HasColumnName("Address")
                .HasMaxLength(100);

            builder.Property(e => e.CityId)
                .HasColumnName("CityID")
                .HasColumnType("smallint");

            builder.Property(e => e.ZipCode)
                .HasColumnName("ZipCode")
                .HasMaxLength(5);

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

            builder.Property(e => e.SalesUnitFlag)
                .HasColumnName("SalesUnitFlag")
                .HasMaxLength(1);

            builder.Property(e => e.ServiceFlag)
                .HasColumnName("ServiceFlag")
                .HasMaxLength(1);

            builder.Property(e => e.SparepartFlag)
                .HasColumnName("SparepartFlag")
                .HasMaxLength(1);

            builder.Property(e => e.Area1Id)
                .HasColumnName("Area1ID");

            builder.Property(e => e.Area2Id)
                .HasColumnName("Area2ID");

            builder.Property(e => e.SPANumber)
                .HasColumnName("SPANumber")
                .HasMaxLength(50);

            builder.Property(e => e.SPADate)
                .HasColumnName("SPADate")
                .HasColumnType("datetime");

            builder.Property(e => e.FreePPh22Indicator)
                .HasColumnName("FreePPh22Indicator");

            builder.Property(e => e.FreePPh22From)
                .HasColumnName("FreePPh22From")
                .HasColumnType("datetime");                

            builder.Property(e => e.FreePPh22To)
                .HasColumnName("FreePPh22To")
                .HasColumnType("datetime");                

            builder.Property(e => e.LegalStatus)
                .HasColumnName("LegalStatus")
                .HasMaxLength(20);

            builder.Property(e => e.DueDate)
                .HasColumnName("DueDate");

            builder.Property(e => e.AgreementNo)
                .HasColumnName("AgreementNo")
                .HasMaxLength(50);
            
            builder.Property(e => e.AgreementDate)
                .HasColumnName("AgreementDate")
                .HasColumnType("datetime");
            
            builder.Property(e => e.LegalStatus)
                .HasColumnName("LegalStatus")
                .HasMaxLength(20);

            builder.Property(e => e.CreditAccount)
                .HasColumnName("LegalStatus")
                .HasMaxLength(20);

            builder.Property(e => e.LegalStatus)
                .HasColumnName("LegalStatus")
                .HasMaxLength(20);

            builder.Property(e => e.DueDate)
                .HasColumnName("DueDate");

            builder.Property(e => e.AgreementNo)
                .HasColumnName("AgreementNo")
                .HasMaxLength(50);

            builder.Property(e => e.AgreementDate)
                .HasColumnName("AgreementDate")
                .HasColumnType("datetime");
            
            builder.Property(e => e.CreditAccount)
                .HasColumnName("CreditAccount")
                .HasMaxLength(6);

            builder.Property(e => e.MainAreaId)
                .HasColumnName("MainAreaID");

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
            builder.ToTable("Dealer");
        }
    }
}
