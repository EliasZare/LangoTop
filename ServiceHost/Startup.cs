using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using _0_Framework.Application;
using _0_Framework.Application.Email;
using _0_Framework.Application.ZarinPal;
using _0_Framework.Infrastructure;
using _01_Query.Contracts;
using _01_Query.Contracts.Account;
using _01_Query.Contracts.Article;
using _01_Query.Contracts.ArticleCategory;
using _01_Query.Contracts.Banner;
using _01_Query.Contracts.Course;
using _01_Query.Contracts.CourseCategory;
using _01_Query.Contracts.Order;
using _01_Query.Query;
using LangoTop.Application;
using LangoTop.Application.Contract.Account;
using LangoTop.Application.Contract.Article;
using LangoTop.Application.Contract.ArticleCategory;
using LangoTop.Application.Contract.Banner;
using LangoTop.Application.Contract.Comment;
using LangoTop.Application.Contract.Course;
using LangoTop.Application.Contract.CourseCategory;
using LangoTop.Application.Contract.CustomerDiscount;
using LangoTop.Application.Contract.DiscountCode;
using LangoTop.Application.Contract.Order;
using LangoTop.Application.Contract.Part;
using LangoTop.Application.Contract.Role;
using LangoTop.Application.Contract.Section;
using LangoTop.Infrastructure;
using LangoTop.Infrastructure.Repository;
using LangoTop.Infrastructure.Repository.Permissions;
using LangoTop.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ServiceHost
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            var connectionString = Configuration.GetConnectionString("LangoTop_DB");

            services.AddDbContext<LangoTopContext>(x => x.UseSqlServer(connectionString));

            #region Transient

            services.AddTransient<IAccountApplication, AccountApplication>();
            services.AddTransient<IAccountRepository, AccountRepository>();

            services.AddTransient<ICourseCategoryApplication, CourseCategoryApplication>();
            services.AddTransient<ICourseCategoryRepository, CourseCategoryRepository>();

            services.AddTransient<ICourseApplication, CourseApplication>();
            services.AddTransient<ICourseRepository, CourseRepository>();

            services.AddTransient<IArticleCategoryApplication, ArticleCategoryApplication>();
            services.AddTransient<IArticleCategoryRepository, ArticleCategoryRepository>();

            services.AddTransient<IArticleApplication, ArticleApplication>();
            services.AddTransient<IArticleRepository, ArticleRepository>();

            services.AddTransient<ICustomerDiscountApplication, CustomerDiscountApplication>();
            services.AddTransient<ICustomerDiscountRepository, CustomerDiscountRepository>();

            services.AddTransient<IDiscountCodeApplication, DiscountCodeApplication>();
            services.AddTransient<IDiscountCodeRepository, DiscountCodeRepository>();

            services.AddTransient<ICommentApplication, CommentApplication>();
            services.AddTransient<ICommentRepository, CommentRepository>();

            services.AddTransient<ISectionApplication, SectionApplication>();
            services.AddTransient<ISectionRepository, SectionRepository>();

            services.AddTransient<IPartApplication, PartApplication>();
            services.AddTransient<IPartRepository, PartRepository>();

            services.AddTransient<IOrderApplication, OrderApplication>();
            services.AddTransient<IOrderRepository, OrderRepository>();

            services.AddTransient<ICourseCategoryQuery, CourseCategoryQuery>();
            services.AddTransient<IArticleQuery, ArticleQuery>();
            services.AddTransient<IArticleCategoryQuery, ArticleCategoryQuery>();
            services.AddTransient<ICourseQuery, CourseQuery>();

            services.AddTransient<IRoleApplication, RoleApplication>();
            services.AddTransient<IRoleRepository, RoleRepository>();

            services.AddTransient<IPermissionExposer, AccountPermissionExposer>();

            services.AddTransient<IOrderApplication, OrderApplication>();
            services.AddTransient<IOrderRepository, OrderRepository>();

            services.AddTransient<IBannerApplication, BannerApplication>();
            services.AddTransient<IBannerRepository, BannerRepository>();

            services.AddTransient<IOrderQuery, OrderQuery>();

            services.AddTransient<IAccountQuery, AccountQuery>();

            services.AddTransient<IBannerQuery, BannerQuery>();

            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddTransient<IFileUploader, FileUploader>();
            services.AddTransient<IAuthHelper, AuthHelper>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IEncryption, Encryption>();
            services.AddSingleton<ICartService, CartService>();
            services.AddTransient<ICartCalculateService, CartCalculateService>();
            services.AddTransient<IZarinPalFactory, ZarinPalFactory>();

            #endregion

            services.Configure<CookiePolicyOptions>(options =>
            {
                //options.CheckConsentNeeded = context => true;
                //options.MinimumSameSitePolicy = SameSiteMode.Lax;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
                {
                    o.LoginPath = new PathString("/Login");
                    o.LogoutPath = new PathString("/Login");
                    o.AccessDeniedPath = new PathString("/AccessDenied");
                });


            services.AddAuthorization(option =>
            {
                option.AddPolicy("AdminArea",
                    builder => builder.RequireRole(new List<string> {Roles.Administrator, Roles.Admin, Roles.Teacher}));

                option.AddPolicy("Courses",
                    builder => builder.RequireRole(new List<string> {Roles.Administrator}));

                option.AddPolicy("Discount",
                    builder => builder.RequireRole(new List<string> {Roles.Administrator}));

                option.AddPolicy("Account",
                    builder => builder.RequireRole(new List<string> {Roles.Administrator, Roles.Teacher}));

                option.AddPolicy("Blog",
                    builder => builder.RequireRole(new List<string> {Roles.Administrator}));

                option.AddPolicy("Order",
                    builder => builder.RequireRole(new List<string> {Roles.Administrator}));

                option.AddPolicy("Messages",
                    builder => builder.RequireRole(new List<string> {Roles.Administrator}));

                option.AddPolicy("DashboardArea",
                    builder => builder.RequireRole(new List<string>
                        {Roles.Administrator, Roles.SystemUser, Roles.Teacher, Roles.Admin}));
            });
            services.AddRazorPages()
                .AddMvcOptions(options => options.Filters.Add<SecurityPageFilter>())
                .AddRazorPagesOptions(option =>
                {
                    option.Conventions.AuthorizeAreaFolder("Administration", "/", "AdminArea");
                    option.Conventions.AuthorizeAreaFolder("Administration", "/Courses", "Courses");
                    option.Conventions.AuthorizeAreaFolder("Administration", "/Discounts", "Discount");
                    option.Conventions.AuthorizeAreaFolder("Administration", "/Accounts", "Account");
                    option.Conventions.AuthorizeAreaFolder("Administration", "/Blog", "Blog");
                    option.Conventions.AuthorizeAreaFolder("Administration", "/Order", "Order");
                    option.Conventions.AuthorizeAreaFolder("Administration", "/Messages", "Messages");
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapRazorPages(); });
        }
    }
}
