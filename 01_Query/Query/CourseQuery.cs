using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_Query.Contracts.Comment;
using _01_Query.Contracts.Course;
using _01_Query.Contracts.Order;
using _01_Query.Contracts.Part;
using _01_Query.Contracts.Section;
using LangoTop.Application.Contract.Comment;
using LangoTop.Domain;
using LangoTop.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace _01_Query.Query
{
    public class CourseQuery : ICourseQuery
    {
        private readonly LangoTopContext _context;
        private readonly IOrderQuery _orderQuery;
        private readonly IAuthHelper _authHelper;

        public CourseQuery(LangoTopContext context, IOrderQuery orderQuery, IAuthHelper authHelper)
        {
            _context = context;
            _orderQuery = orderQuery;
            _authHelper = authHelper;
        }

        public List<CourseQueryModel> LatestCourses(int counts)
        {
            var discounts = _context.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new {x.DiscountRate, x.CourseId}).ToList();


            var courses = _context.Courses.Include(x => x.CourseCategory).Include(x => x.Teacher).Select(x =>
                new CourseQueryModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    PictureTitle = x.PictureTitle,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    Category = x.CourseCategory.Name,
                    Slug = x.Slug,
                    IsRemoved = x.IsRemoved,
                    PictureSmall = x.PictureSmall,
                    Level = x.Level,
                    Time = x.Time,
                    Price = x.Price.ToMoney(),
                    TeacherProfile = x.Teacher.ProfilePhoto,
                    Teacher = x.Teacher.Fullname,
                    DoublePrice = x.Price,
                    TeacherUsername = x.Teacher.Username,
                    ShortLink = x.ShortLink
                }).Where(x => !x.IsRemoved).OrderByDescending(x => x.Id).Take(counts).ToList();


            foreach (var course in courses)
            {
                var discount = discounts.FirstOrDefault(x => x.CourseId == course.Id);
                if (discount != null)
                {
                    var discountRate = discount.DiscountRate;
                    course.DiscountRate = discountRate;
                    course.HasDiscount = discountRate > 0;
                    var discountAmount = Math.Round(course.DoublePrice * discountRate / 100);
                    course.PriceWithDiscount = (course.DoublePrice - discountAmount).ToMoney();
                }
            }

            return courses;
        }

        public List<CourseQueryModel> GetCourses()
        {
            var discounts = _context.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new {x.DiscountRate, x.CourseId}).ToList();


            var products = _context.Courses.Include(x => x.CourseCategory).Include(x => x.Teacher).Select(x =>
                new CourseQueryModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    PictureTitle = x.PictureTitle,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    Category = x.CourseCategory.Name,
                    Slug = x.Slug,
                    IsRemoved = x.IsRemoved,
                    PictureSmall = x.PictureSmall,
                    Level = x.Level,
                    Time = x.Time,
                    Price = x.Price.ToMoney(),
                    TeacherProfile = x.Teacher.ProfilePhoto,
                    Teacher = x.Teacher.Fullname,
                    DoublePrice = x.Price,
                    TeacherUsername = x.Teacher.Username,
                    ShortLink = x.ShortLink
                }).Where(x => !x.IsRemoved).OrderByDescending(x => x.Id).ToList();


            foreach (var product in products)
            {
                var discount = discounts.FirstOrDefault(x => x.CourseId == product.Id);
                if (discount != null)
                {
                    var discountRate = discount.DiscountRate;
                    product.DiscountRate = discountRate;
                    product.HasDiscount = discountRate > 0;
                    var discountAmount = Math.Round(product.DoublePrice * discountRate / 100);
                    product.PriceWithDiscount = (product.DoublePrice - discountAmount).ToMoney();
                }
            }

            return products;
        }

        public PagingCourseQueryModel GetCourses(int pageId = 1)
        {
            IQueryable<Course> result = _context.Courses; //----lazy load

            //-------paging---------//
            var take = 9;
            var skip = (pageId - 1) * take;

            var list = new PagingCourseQueryModel();
            list.CurrentPage = pageId;
            list.PageCount = (int) Math.Ceiling(result.Count() / (double) take);
            var discounts = _context.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new {x.DiscountRate, x.CourseId}).ToList();


            list.Courses = _context.Courses.Include(x => x.CourseCategory).Include(x => x.Teacher).Select(x =>
                new CourseQueryModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    PictureTitle = x.PictureTitle,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    Category = x.CourseCategory.Name,
                    Slug = x.Slug,
                    IsRemoved = x.IsRemoved,
                    PictureSmall = x.PictureSmall,
                    Level = x.Level,
                    Time = x.Time,
                    Price = x.Price.ToMoney(),
                    TeacherProfile = x.Teacher.ProfilePhoto,
                    Teacher = x.Teacher.Fullname,
                    DoublePrice = x.Price,
                    ShortLink = x.ShortLink
                }).Where(x => !x.IsRemoved).OrderByDescending(x => x.Id).Skip(skip).Take(take).ToList();


            foreach (var product in list.Courses)
            {
                var discount = discounts.FirstOrDefault(x => x.CourseId == product.Id);
                if (discount != null)
                {
                    var discountRate = discount.DiscountRate;
                    product.DiscountRate = discountRate;
                    product.HasDiscount = discountRate > 0;
                    var discountAmount = Math.Round(product.DoublePrice * discountRate / 100);
                    product.PriceWithDiscount = (product.DoublePrice - discountAmount).ToMoney();
                }
            }

            return list;
        }

        public PagingCourseQueryModel Search(string searchModel, int pageId)
        {
            IQueryable<Course> result = _context.Courses; //----lazy load

            //-------paging---------//
            var take = 9;
            var skip = (pageId - 1) * take;

            var list = new PagingCourseQueryModel();
            list.CurrentPage = pageId;


            var discounts = _context.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new {x.DiscountRate, x.CourseId}).ToList();


            var query = _context.Courses.Include(x => x.CourseCategory).Include(x => x.Teacher).Select(x =>
                new CourseQueryModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    PictureTitle = x.PictureTitle,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    Category = x.CourseCategory.Name,
                    Slug = x.Slug,
                    IsRemoved = x.IsRemoved,
                    PictureSmall = x.PictureSmall,
                    Level = x.Level,
                    Time = x.Time,
                    Price = x.Price.ToMoney(),
                    TeacherProfile = x.Teacher.ProfilePhoto,
                    Teacher = x.Teacher.Fullname,
                    ShortDescription = x.ShortDescription,
                    Keywords = x.Keywords,
                    DoublePrice = x.Price,
                    ShortLink = x.ShortLink
                }).Where(x => !x.IsRemoved).AsNoTracking().AsEnumerable();

            if (!string.IsNullOrWhiteSpace(searchModel))
                query = query.Where(x =>
                    x.Title.Contains(searchModel) || x.ShortDescription.Contains(searchModel) ||
                    x.Keywords.Contains(searchModel));

            var products = query.OrderByDescending(x => x.Id).ToList();

            foreach (var course in products)
            {
                var discount = discounts.FirstOrDefault(x => x.CourseId == course.Id);
                if (discount != null)
                {
                    var discountRate = discount.DiscountRate;
                    course.DiscountRate = discountRate;
                    course.HasDiscount = discountRate > 0;
                    var discountAmount = Math.Round(course.DoublePrice * discountRate / 100);
                    course.PriceWithDiscount = (course.DoublePrice - discountAmount).ToMoney();
                }
            }

            list.PageCount = (int) Math.Ceiling(products.Count() / (double) take);
            list.Courses = products.Skip(skip).Take(take).ToList();
            return list;
        }

        public CourseQueryModel GetDetails(string slug)
        {
            var discounts = _context.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new {x.DiscountRate, x.CourseId}).ToList();

            var accountId = _authHelper.CurrentAccountId();

            var paidCourses = _orderQuery.GetCoursesBy(accountId);

            var course = _context.Courses.Include(x => x.CourseCategory).Include(x => x.Teacher).Select(x =>
                new CourseQueryModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    PictureTitle = x.PictureTitle,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    Category = x.CourseCategory.Name,
                    Slug = x.Slug,
                    IsRemoved = x.IsRemoved,
                    PictureSmall = x.PictureSmall,
                    Level = x.Level,
                    Time = x.Time,
                    Price = x.Price.ToMoney(),
                    TeacherProfile = x.Teacher.ProfilePhoto,
                    Teacher = x.Teacher.Fullname,
                    ShortDescription = x.ShortDescription,
                    Keywords = x.Keywords,
                    DoublePrice = x.Price,
                    CategorySlug = x.CourseCategory.Slug,
                    Description = x.Description,
                    MetaDescription = x.MetaDescription,
                    TeacherCourseCount = x.Teacher.Courses.Count,
                    TeacherJoinDate = x.Teacher.CreationDate.ToFarsi(),
                    TeacherId = x.TeacherId,
                    PageTitle = x.PageTitle,
                    TeacherBio = x.Teacher.Biography,
                    TeacherUsername = x.Teacher.Username,
                    Sections = MapSections(x.Sections),
                    ShortLink = x.ShortLink
                }).Where(x => !x.IsRemoved).FirstOrDefault(x => x.Slug == slug);

            foreach (var paidCourse in paidCourses)
                if (paidCourse.Id == course.Id)
                    course.IsPaid = true;


            if (course == null) return new CourseQueryModel();


            if (!string.IsNullOrWhiteSpace(course.Keywords))
                course.KeywordList = course.Keywords.Split("،").ToList();

            course.StudentsCount = _orderQuery.GetStudentCourseCount(course.Id);

            var partCount = 0;
            foreach (var section in course.Sections)
            {
                var parts = _context.Parts
                    .Select(x => new PartQueryModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        IsRemoved = x.IsRemoved,
                        SectionId = x.SectionId,
                        Time = x.Time,
                        DownloadLink = x.DownloadLink
                    }).OrderBy(x => x.Id).Where(x => !x.IsRemoved && x.SectionId == section.Id).ToList();

                section.Parts = parts;
                partCount += parts.Count;
            }

            course.PartCount = partCount;

            var discount = discounts.FirstOrDefault(x => x.CourseId == course.Id);
            if (discount != null)
            {
                var discountRate = discount.DiscountRate;
                course.DiscountRate = discountRate;
                course.HasDiscount = discountRate > 0;
                var discountAmount = Math.Round(course.DoublePrice * discountRate / 100);
                course.PriceWithDiscount = (course.DoublePrice - discountAmount).ToMoney();
            }


            course.Comments = _context.Comments
                .Where(x => !x.IsCanceled)
                .Where(x => x.IsConfirmed)
                .Where(x => x.Type == CommentType.Course)
                .Where(x => x.OwnerRecordId == course.Id)
                .Select(x => new CommentQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Message = x.Message,
                    CreationDate = x.CreationDate.ToFarsi()
                }).OrderByDescending(x => x.Id).ToList();


            return course;
        }

        public List<CourseQueryModel> GetCoursesBy(long teacherId)
        {
            var discounts = _context.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new {x.DiscountRate, x.CourseId}).ToList();


            var products = _context.Courses.Where(x => x.TeacherId == teacherId).Include(x => x.CourseCategory)
                .Include(x => x.Teacher).Select(x =>
                    new CourseQueryModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        PictureTitle = x.PictureTitle,
                        Picture = x.Picture,
                        PictureAlt = x.PictureAlt,
                        Category = x.CourseCategory.Name,
                        Slug = x.Slug,
                        IsRemoved = x.IsRemoved,
                        PictureSmall = x.PictureSmall,
                        Level = x.Level,
                        Time = x.Time,
                        Price = x.Price.ToMoney(),
                        TeacherProfile = x.Teacher.ProfilePhoto,
                        Teacher = x.Teacher.Fullname,
                        DoublePrice = x.Price,
                        TeacherUsername = x.Teacher.Username,
                        ShortLink = x.ShortLink
                    }).Where(x => !x.IsRemoved).OrderByDescending(x => x.Id).ToList();


            foreach (var product in products)
            {
                var discount = discounts.FirstOrDefault(x => x.CourseId == product.Id);
                if (discount != null)
                {
                    var discountRate = discount.DiscountRate;
                    product.DiscountRate = discountRate;
                    product.HasDiscount = discountRate > 0;
                    var discountAmount = Math.Round(product.DoublePrice * discountRate / 100);
                    product.PriceWithDiscount = (product.DoublePrice - discountAmount).ToMoney();
                }
            }

            return products;
        }

        public List<CourseQueryModel> GetCoursesBy(string username)
        {
            var discounts = _context.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new {x.DiscountRate, x.CourseId}).ToList();


            var products = _context.Courses.Where(x => x.Teacher.Username == username).Include(x => x.CourseCategory)
                .Include(x => x.Teacher).Select(x =>
                    new CourseQueryModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        PictureTitle = x.PictureTitle,
                        Picture = x.Picture,
                        PictureAlt = x.PictureAlt,
                        Category = x.CourseCategory.Name,
                        Slug = x.Slug,
                        IsRemoved = x.IsRemoved,
                        PictureSmall = x.PictureSmall,
                        Level = x.Level,
                        Time = x.Time,
                        Price = x.Price.ToMoney(),
                        TeacherProfile = x.Teacher.ProfilePhoto,
                        Teacher = x.Teacher.Fullname,
                        DoublePrice = x.Price
                    }).Where(x => !x.IsRemoved).OrderByDescending(x => x.Id).ToList();


            foreach (var product in products)
            {
                var discount = discounts.FirstOrDefault(x => x.CourseId == product.Id);
                if (discount != null)
                {
                    var discountRate = discount.DiscountRate;
                    product.DiscountRate = discountRate;
                    product.HasDiscount = discountRate > 0;
                    var discountAmount = Math.Round(product.DoublePrice * discountRate / 100);
                    product.PriceWithDiscount = (product.DoublePrice - discountAmount).ToMoney();
                }
            }

            return products;
        }


        public static List<SectionQueryModel> MapSections(List<Section> section)
        {
            if (section == null) return new List<SectionQueryModel>();
            return section.Select(x => new SectionQueryModel
            {
                Id = x.Id,
                CourseId = x.CourseId,
                Title = x.Title,
                IsRemoved = x.IsRemoved
            }).OrderBy(x => x.Id).ToList();
        }
    }
}
