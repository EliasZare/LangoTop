using _01_Query.Contracts.CourseCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class CourseCategoryModel : PageModel
    {
        public CourseCategoryQueryModel CourseCategory;
        private readonly ICourseCategoryQuery _courseCategoryQuery;

        public CourseCategoryModel(ICourseCategoryQuery courseCategoryQuery)
        {
            _courseCategoryQuery = courseCategoryQuery;
        }

        public void OnGet(string id)
        {
            CourseCategory = _courseCategoryQuery.GetProductCategoryWithProductsBy(id);
        }
    }
}
