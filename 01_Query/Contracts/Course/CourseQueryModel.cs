using System.Collections.Generic;
using _01_Query.Contracts.Comment;
using _01_Query.Contracts.Section;

namespace _01_Query.Contracts.Course
{
    public class CourseQueryModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string PictureSmall { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public double DoublePrice { get; set; }
        public string Price { get; set; }
        public string PriceWithDiscount { get; set; }
        public int DiscountRate { get; set; }
        public long StudentsCount { get; set; }
        public string Slug { get; set; }
        public string Category { get; set; }
        public string CategorySlug { get; set; }
        public string Teacher { get; set; }
        public string TeacherProfile { get; set; }
        public long TeacherId { get; set; }
        public bool IsRemoved { get; set; }
        public bool IsLocked { get; set; }
        public string Time { get; set; }
        public long PartCount { get; set; }
        public string Level { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        public bool HasDiscount { get; set; }
        public string DiscountExpireDate { get; set; }
        public long TeacherCourseCount { get; set; }
        public string TeacherJoinDate { get; set; }
        public string TeacherInstagramLink { get; set; }
        public string TeacherTwitterLink { get; set; }
        public string TeacherBio { get; set; }
        public List<string> KeywordList { get; set; }
        public List<CommentQueryModel> Comments { get; set; }
        public List<SectionQueryModel> Sections { get; set; }
        public string PageTitle { get; set; }
    }
}
