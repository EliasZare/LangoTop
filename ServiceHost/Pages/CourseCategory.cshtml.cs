using _01_Query.Contracts.Course;
using _01_Query.Contracts.CourseCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class CourseCategoryModel : PageModel
    {
        public CourseCategoryQueryModel CourseCategory;
        private readonly ICourseCategoryQuery _courseCategoryQuery;
        private readonly ICourseQuery _courseQuery;
        public PagingCourseQueryModel Courses { get; set; }

        [TempData] public string PageId { get; set; }

        public CourseCategoryModel(ICourseCategoryQuery courseCategoryQuery, ICourseQuery courseQuery)
        {
            _courseCategoryQuery = courseCategoryQuery;
            _courseQuery = courseQuery;
        }

        public void OnGet(string slug, int id = 1)
        {
            PageId = id.ToString();
            Courses = _courseQuery.GetCoursesBy(slug, id);
            CourseCategory = _courseCategoryQuery.GetProductCategoryWithProductsBy(slug);
        }
    }
}
