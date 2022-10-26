using _01_Query.Contracts.Course;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class LatestCoursesViewComponent : ViewComponent
    {
        private readonly ICourseQuery _courseQuery;

        public LatestCoursesViewComponent(ICourseQuery courseQuery)
        {
            _courseQuery = courseQuery;
        }

        public IViewComponentResult Invoke()
        {
            var courses = _courseQuery.LatestCourses(6);
            return View(courses);
        }
    }
}
