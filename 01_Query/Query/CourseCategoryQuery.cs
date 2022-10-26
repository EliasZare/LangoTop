using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_Query.Contracts.Course;
using _01_Query.Contracts.CourseCategory;
using LangoTop.Domain;
using LangoTop.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace _01_Query.Query
{
    public class CourseCategoryQuery : ICourseCategoryQuery
    {
        private readonly LangoTopContext _context;

        public CourseCategoryQuery(LangoTopContext context)
        {
            _context = context;
        }

        public List<CourseCategoryQueryModel> GetCourseCategories()
        {
            return _context.CourseCategories
                .Where(x => !x.IsRemoved)
                .Select(x => new CourseCategoryQueryModel
                {
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    Id = x.Id,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    IsRemoved = x.IsRemoved,
                    Description = x.Description
                }).ToList();
        }

        public CourseCategoryQueryModel GetProductCategoryWithProductsBy(string slug)
        {
            var teachers = _context.Accounts.Select(x => new {x.Id, x.Fullname, x.ProfilePhoto}).ToList();

            var discounts = _context.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new {x.DiscountRate, x.CourseId, x.EndDate}).ToList();


            var category = _context.CourseCategories
                .Where(x => !x.IsRemoved)
                .Include(x => x.Courses)
                .ThenInclude(x => x.CourseCategory)
                .Select(x =>
                    new CourseCategoryQueryModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Keywords = x.Keywords,
                        MetaDescription = x.MetaDescription,
                        Slug = x.Slug,
                        IsRemoved = x.IsRemoved,
                        Courses = MapCourses(x.Courses)
                    }).FirstOrDefault(x => x.Slug == slug);

            foreach (var course in category.Courses)
            {
                var a = course.TeacherId;
                var courseTeacher = teachers.FirstOrDefault(x => x.Id == course.TeacherId);
                if (courseTeacher != null)
                {
                    course.Teacher = courseTeacher.Fullname;
                    course.TeacherProfile = courseTeacher.ProfilePhoto;
                    var discount = discounts.FirstOrDefault(x => x.CourseId == course.Id);
                    if (discount != null)
                    {
                        var discountRate = discount.DiscountRate;
                        course.DiscountRate = discountRate;
                        course.HasDiscount = discountRate > 0;
                        var discountAmount = Math.Round(course.DoublePrice * discountRate / 100);
                        course.PriceWithDiscount = (course.DoublePrice - discountAmount).ToMoney();
                        course.DiscountExpireDate = discount.EndDate.ToDiscountFormat();
                    }
                }
            }

            return category;
        }

        public static List<CourseQueryModel> MapCourses(List<Course> course)
        {
            return course.Select(x => new CourseQueryModel
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
                DoublePrice = x.Price,
                TeacherId = x.TeacherId
            }).ToList();
        }
    }
}
