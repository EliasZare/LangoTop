using System.Collections.Generic;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using _01_Query.Contracts.Course;
using _01_Query.Contracts.Order;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Dashboard.Pages
{
    public class CoursesModel : PageModel
    {
        private readonly ICourseQuery _courseQuery;
        private readonly IOrderQuery _orderQuery;
        private readonly IAuthHelper _authHelper;
        public List<CourseQueryModel> Courses;

        public CoursesModel(ICourseQuery courseQuery, IOrderQuery orderQuery, IAuthHelper authHelper)
        {
            _courseQuery = courseQuery;
            _orderQuery = orderQuery;
            _authHelper = authHelper;
        }

        public void OnGet()
        {
            var account = _authHelper.CurrentAccountInfo();
            var accountRole = _authHelper.CurrentAccountRole();

            if (accountRole == Roles.Teacher)
                Courses = _courseQuery.GetCoursesBy(account.Id);
            else
                Courses = _orderQuery.GetCoursesBy(account.Id);
        }
    }
}
