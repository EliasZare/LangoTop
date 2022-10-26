using System.Collections.Generic;
using _01_Query.Contracts.Course;

namespace _01_Query.Contracts.CourseCategory
{
    public class CourseCategoryQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Slug { get; set; }
        public string Keywords { get; set; }
        public List<string> KeywordList { get; set; }
        public string MetaDescription { get; set; }
        public string Description { get; set; }
        public bool IsRemoved { get; set; }
        public List<CourseQueryModel> Courses { get; set; }
    }
}
