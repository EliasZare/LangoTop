using LangoTop.Application.Contract.Account;
using LangoTop.Application.Contract.Course;
using LangoTop.Application.Contract.CourseCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Courses.Course
{
    public class CreateModel : PageModel
    {
        private readonly ICourseApplication _courseApplication;
        private readonly ICourseCategoryApplication _courseCategoryApplication;
        private readonly IAccountApplication _accountApplication;
        public CreateCourse Command;
        public SelectList Teachers { get; set; }
        public SelectList CourseCategories { get; set; }


        public CreateModel(ICourseApplication courseApplication, ICourseCategoryApplication courseCategoryApplication,
            IAccountApplication accountApplication)
        {
            _courseApplication = courseApplication;
            _courseCategoryApplication = courseCategoryApplication;
            _accountApplication = accountApplication;
        }

        public void OnGet()
        {
            CourseCategories = new SelectList(_courseCategoryApplication.GetCourseCategories(), "Id", "Name");
            Teachers = new SelectList(_accountApplication.GetAdmins(), "Id", "Fullname");
        }

        public IActionResult OnPost(CreateCourse command)
        {
            _courseApplication.Create(command);
            return RedirectToPage("/Index");
        }
    }
}
