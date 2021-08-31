using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFIDWebAPI.Domain.Entities;

namespace SFIDWebAPI.Infrastructure.Persistences.Configurations
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(d => d.MenuType)
                .WithMany(t => t.Menus)
                .HasForeignKey(d => d.MenuTypeId);

            builder.HasMany(d => d.Permissions)
                .WithOne(p => p.Menu)
                .HasForeignKey(d => d.MenuId);

            builder.Property(e => e.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.Description)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.MenuTypeId)
                .HasColumnName("SFDMenuTypeID")
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

            builder.Property(e => e.RowStatus)
                .HasColumnName("RowStatus");

            builder.HasQueryFilter(m => m.RowStatus == 0);
            builder.ToTable("SFDMenu");
        }
    }
}
