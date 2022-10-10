using System.Collections.Generic;
using _0_Framework.Domain;
using LangoTop.Application.Contract.Account;
using LangoTop.Application.Contract.CourseCategory;
using Microsoft.AspNetCore.Http;

namespace LangoTop.Application.Contract.Course
{
    public class CreateCourse : EntityBase
    {
        public string Title { get; set; }
        public string PageTitle { get; set; }
        public long TeacherId { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public IFormFile Picture { get; set; }
        public IFormFile PictureSmall { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Level { get; set; }
        public string Time { get; set; }
        public double Price { get; set; }
        public long CategoryId { get; set; }
        public bool IsRemoved { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }
        public string ShortLink { get; set; }
        public List<CourseCategoryViewModel> CourseCategories { get; set; }
        public List<AccountViewModel> Teachers { get; set; }
    }
}