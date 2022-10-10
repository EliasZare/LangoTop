using System.Collections.Generic;
using _0_Framework.Domain;
using LangoTop.Application.Contract.Course;
using LangoTop.Domain;

namespace LangoTop.Interfaces
{
    public interface ICourseRepository : IRepository<long, Course>
    {
        EditCourse GetDetails(long id);
        List<CourseViewModel> GetCourses();
        List<CourseViewModel> Search(CourseSearchModel searchModel);
    }
}
