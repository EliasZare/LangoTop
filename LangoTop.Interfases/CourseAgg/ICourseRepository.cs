using System.Collections.Generic;
using _0_Framework.Domain;
using LangoTop.Application.Contract.Course;
using LangoTop.Domain.CourseAgg;

namespace LangoTop.Interfaces.CourseAgg
{
    public interface ICourseRepository : IRepository<long, Course>
    {
        EditCourse GetDetails(long id);
        List<CourseViewModel> GetCourses();
        List<CourseViewModel> Search(CourseSearchModel searchModel);
    }
}
