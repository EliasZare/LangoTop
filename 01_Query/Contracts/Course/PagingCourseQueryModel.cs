using System.Collections.Generic;

namespace _01_Query.Contracts.Course
{
    public class PagingCourseQueryModel
    {
        public List<CourseQueryModel> Courses { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}
