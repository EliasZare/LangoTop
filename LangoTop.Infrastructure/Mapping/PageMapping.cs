using LangoTop.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LangoTop.Infrastructure.Mapping
{
    internal class PageMapping : IEntityTypeConfiguration<Page>
    {
        public void Configure(EntityTypeBuilder<Page> builder)
        {
            builder.ToTable("Pages");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Slug).IsRequired();
            builder.Property(x => x.ShortKey).HasMaxLength(5).IsRequired();
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Type).IsRequired();
        }
    }
}
