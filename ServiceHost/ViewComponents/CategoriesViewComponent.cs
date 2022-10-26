using _01_Query.Contracts.CourseCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly ICourseCategoryQuery _courseCategory;

        public CategoriesViewComponent(ICourseCategoryQuery courseCategory)
        {
            _courseCategory = courseCategory;
        }

        public IViewComponentResult Invoke()
        {
            var courseCategories = _courseCategory.GetCourseCategories();
            return View(courseCategories);
        }
    }
}
