using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFIDWebAPI.Domain.Entities;

namespace SFIDWebAPI.Infrastructure.Persistences.Configurations
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(d => d.Role)
                .WithMany(c => c.Admins)
                .HasForeignKey(d => d.RoleId);

            builder.Property(e => e.Name)
                .HasColumnName("Name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.Email)
                .HasColumnName("Email")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.Phone)
                .HasColumnName("Phone")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.Password)
                .HasColumnName("Password")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.RoleId)
                .HasColumnName("RoleId")
                .IsRequired();

            builder.Property(e => e.LastLogin)
                .HasColumnName("LastLogin")
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

            builder.Property(e => e.RowStatus)
                .HasColumnName("RowStatus");

            builder.HasQueryFilter(m => m.RowStatus == 0);
            builder.ToTable("SFDAdmin");
        }
    }
}
