using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFIDWebAPI.Domain.Entities;
using SFIDWebAPI.Application.Misc;

namespace SFIDWebAPI.Infrastructure.Persistences.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.Property(e => e.Id)
                .HasColumnName("ID");

            builder.Property(e => e.ProvinceId)
                .HasColumnName("ProvinceID");

            builder.Property(e => e.CityCode)
                .HasColumnName("CityCode")
                .HasMaxLength(10);

            builder.Property(e => e.CityName)
                .HasColumnName("CityName")
                .HasMaxLength(40);

            builder.Property(e => e.LastUpdateTime)
                .HasColumnName("LastUpdateTime")
                .HasColumnType("datetime");

            builder.Property(e => e.Status)
                .HasColumnName("Status")
                .HasMaxLength(1);
                
            builder.ToTable("City");
        }
    }
}
