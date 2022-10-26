using System.Collections.Generic;
using _01_Query.Contracts.CourseCategory;

namespace _01_Query.Contracts
{
    public class MenuQueryModel
    {
        public List<CourseCategoryQueryModel> CourseCategories { get; set; }
    }
}
