﻿using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using LangoTop.Application.Contract.Course;
using LangoTop.Domain;
using LangoTop.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LangoTop.Infrastructure.Repository
{
    public class CourseRepository : RepositoryBase<long, Course>, ICourseRepository
    {
        private readonly LangoTopContext _context;

        public CourseRepository(LangoTopContext context) : base(context)
        {
            _context = context;
        }


        public EditCourse GetDetails(long id)
        {
            return _context.Courses.Select(x => new EditCourse
            {
                Id = x.Id,
                Title = x.Title,
                PageTitle = x.PageTitle,
                TeacherId = x.TeacherId,
                Description = x.Description,
                ShortDescription = x.ShortDescription,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Price = x.Price,
                Level = x.Level,
                Time = x.Time,
                CategoryId = x.CategoryId,
                IsRemoved = x.IsRemoved,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                Slug = x.Slug,
                ShortLink = x.ShortLink
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<CourseViewModel> GetCourses()
        {
            return _context.Courses.Include(x => x.CourseCategory).Include(x => x.Teacher).Select(x =>
                new CourseViewModel
                {
                    Id = x.Id,
                    PageTitle = x.PageTitle,
                    TeacherId = x.TeacherId,
                    Teacher = x.Teacher.Fullname,
                    Title = x.Title,
                    Price = x.Price,
                    Level = x.Level,
                    Picture = x.Picture,
                    CategoryId = x.CategoryId,
                    IsRemoved = x.IsRemoved,
                    CourseCategory = x.CourseCategory.Name,
                    CreationDate = x.CreationDate.ToFarsi(),
                    ShortLink = x.ShortLink,
                    PictureSmall = x.PictureSmall
                }).OrderByDescending(x => x.Id).ToList();
        }

        public List<CourseViewModel> Search(CourseSearchModel searchModel)
        {
            var query = _context.Courses.Include(x => x.CourseCategory).Include(x => x.Teacher).Select(x =>
                new CourseViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    PageTitle = x.PageTitle,
                    TeacherId = x.TeacherId,
                    Teacher = x.Teacher.Fullname,
                    Price = x.Price,
                    Level = x.Level,
                    Picture = x.Picture,
                    CategoryId = x.CategoryId,
                    IsRemoved = x.IsRemoved,
                    CourseCategory = x.CourseCategory.Name,
                    CreationDate = x.CreationDate.ToFarsi(),
                    ShortLink = x.ShortLink,
                    PictureSmall = x.PictureSmall
                });

            if (!string.IsNullOrWhiteSpace(searchModel.Title))
                query = query.Where(x => x.Title.Contains(searchModel.Title));

            if (searchModel.CategoryId > 0)
                query = query.Where(x => x.CategoryId == searchModel.CategoryId);

            if (searchModel.TeacherId > 0)
                query = query.Where(x => x.TeacherId == searchModel.TeacherId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}