using System.Collections.Generic;
using _0_Framework.Application;
using _01_Query.Contracts.Account;
using _01_Query.Contracts.Course;
using LangoTop.Application.Contract.Account;
using LangoTop.Application.Contract.Course;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly IAccountApplication _accountApplication;
        private readonly IAccountQuery _accountQuery;
        private readonly ICourseQuery _courseQuery;
        private readonly ICourseApplication _courseApplication;
        private readonly IEncryption _encryption;
        public AccountQueryModel Account;
        public List<CourseQueryModel> Courses;

        public ProfileModel(IAccountApplication accountApplication, ICourseQuery courseQuery,
            ICourseApplication courseApplication, IEncryption encryption, IAccountQuery accountQuery)
        {
            _accountApplication = accountApplication;
            _courseQuery = courseQuery;
            _courseApplication = courseApplication;
            _encryption = encryption;
            _accountQuery = accountQuery;
        }

        public void OnGet(string id)
        {
            Courses = _courseQuery.GetCoursesBy(id);
            Account = _accountQuery.GetDetails(id);
        }
    }
}
