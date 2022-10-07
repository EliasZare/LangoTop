using LangoTop.Domain.AccountAgg;
using LangoTop.Domain.ArticleAgg;
using LangoTop.Domain.ArticleCategoryAgg;
using LangoTop.Domain.CourseAgg;
using LangoTop.Domain.CourseCategoryAgg;
using LangoTop.Domain.CustomerDiscountAgg;
using LangoTop.Domain.DiscountCodeAgg;
using Microsoft.EntityFrameworkCore;

namespace LangoTop.Infrastructure
{
    public class LangoTopContext : DbContext
    {
        public DbSet<CourseCategory> CourseCategories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleCategory> ArticleCategories { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<CustomerDiscount> CustomerDiscounts { get; set; }
        public DbSet<DiscountCode> DiscountCodes { get; set; }

        public LangoTopContext(DbContextOptions<LangoTopContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
