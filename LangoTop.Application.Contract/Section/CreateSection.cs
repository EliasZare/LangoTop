using System.Collections.Generic;
using LangoTop.Application.Contract.Course;

namespace LangoTop.Application.Contract.Section
{
    public class CreateSection
    {
        public string Title { get; set; }
        public long CourseId { get; set; }
        public List<CourseViewModel> Courses { get; set; }
    }
}
