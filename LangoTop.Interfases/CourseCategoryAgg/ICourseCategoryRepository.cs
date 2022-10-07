using System.Collections.Generic;
using _0_Framework.Domain;
using LangoTop.Application.Contract.CourseCategory;
using LangoTop.Domain.CourseCategoryAgg;

namespace LangoTop.Interfaces.CourseCategoryAgg
{
    public interface ICourseCategoryRepository : IRepository<long, CourseCategory>
    {
        EditCourseCategory GetDetails(long id);
        List<CourseCategoryViewModel> GetCourseCategories();
        List<CourseCategoryViewModel> Search(CourseCategorySearchModel searchModel);
    }
}
