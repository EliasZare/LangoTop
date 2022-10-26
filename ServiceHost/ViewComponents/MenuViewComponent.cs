using _01_Query.Contracts;
using _01_Query.Contracts.CourseCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly ICourseCategoryQuery _courseCategory;

        public MenuViewComponent(ICourseCategoryQuery courseCategory)
        {
            _courseCategory = courseCategory;
        }

        public IViewComponentResult Invoke()
        {
            var command = new MenuQueryModel
            {
                CourseCategories = _courseCategory.GetCourseCategories()
            };
            return View(command);
        }
    }
}
