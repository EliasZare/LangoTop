using System.Collections.Generic;

namespace _01_Query.Contracts.CourseCategory
{
    public interface ICourseCategoryQuery
    {
        public List<CourseCategoryQueryModel> GetCourseCategories();
        CourseCategoryQueryModel GetProductCategoryWithProductsBy(string slug);
    }
}
