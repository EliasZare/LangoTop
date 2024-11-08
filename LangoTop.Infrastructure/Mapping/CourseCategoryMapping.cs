﻿using LangoTop.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LangoTop.Infrastructure.Mapping
{
    public class CourseCategoryMapping : IEntityTypeConfiguration<CourseCategory>
    {
        public void Configure(EntityTypeBuilder<CourseCategory> builder)
        {
            builder.ToTable("CourseCategories");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.Slug).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Keywords).HasMaxLength(120).IsRequired();
            builder.Property(x => x.Picture).HasMaxLength(1000);
            builder.Property(x => x.PictureAlt).HasMaxLength(125);

            builder
                .HasMany(x => x.Courses)
                .WithOne(x => x.CourseCategory)
                .HasForeignKey(x => x.CategoryId);
        }
    }
}