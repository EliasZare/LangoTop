using LangoTop.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LangoTop.Infrastructure.Mapping
{
    internal class BannerMapping : IEntityTypeConfiguration<Banner>
    {
        public void Configure(EntityTypeBuilder<Banner> builder)
        {
            builder.ToTable("Banners");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.BannerPicture).IsRequired();
            builder.Property(x => x.Title);
        }
    }
}
