using System.Collections.Generic;
using _01_Query.Contracts.Course;

namespace _01_Query.Contracts
{
    public class FooterQueryModel
    {
        public List<CourseQueryModel> LatestCourses { get; set; }
    }
}
