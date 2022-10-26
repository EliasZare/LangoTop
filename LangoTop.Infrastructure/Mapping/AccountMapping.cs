using LangoTop.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LangoTop.Infrastructure.Mapping
{
    public class AccountMapping : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Fullname).IsRequired().HasMaxLength(70);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Mobile).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.Username).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Biography).HasMaxLength(150);

            builder
                .HasMany(x => x.Courses)
                .WithOne(x => x.Teacher)
                .HasForeignKey(x => x.TeacherId);

            builder
                .HasMany(x => x.Articles)
                .WithOne(x => x.Author)
                .HasForeignKey(x => x.AuthorId);

            builder.HasOne(x => x.Role)
                .WithMany(x => x.Accounts)
                .HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}