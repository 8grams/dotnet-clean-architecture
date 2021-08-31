using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFIDWebAPI.Domain.Entities;

namespace SFIDWebAPI.Infrastructure.Persistences.Configurations
{
    public class StaticContentConfiguration : IEntityTypeConfiguration<StaticContent>
    {
        public void Configure(EntityTypeBuilder<StaticContent> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.AppInfo)
                .HasColumnName("AppInfo");

            builder.Property(e => e.Disclaimer)
            	.HasColumnName("Disclaimer");

            builder.Property(e => e.PrivacyPolicy)
            	.HasColumnName("PrivacyPolicy");

            builder.Property(e => e.TermCondition)
                .HasColumnName("TermCondition");
			
			builder.Property(e => e.Tutorial)
				.HasColumnName("Tutorial");

            builder.ToTable("SFDStaticContent");
        }
    }
}
