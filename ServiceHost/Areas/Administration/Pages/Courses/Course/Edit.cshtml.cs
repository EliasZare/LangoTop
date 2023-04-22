using LangoTop.Application.Contract.Account;
using LangoTop.Application.Contract.Course;
using LangoTop.Application.Contract.CourseCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Courses.Course
{
    public class EditModel : PageModel
    {
        private readonly ICourseApplication _courseApplication;
        private readonly ICourseCategoryApplication _courseCategoryApplication;
        private readonly IAccountApplication _accountApplication;
        public EditCourse Command;

        public EditModel(ICourseApplication courseApplication, ICourseCategoryApplication courseCategoryApplication,
            IAccountApplication accountApplication)
        {
            _courseApplication = courseApplication;
            _courseCategoryApplication = courseCategoryApplication;
            _accountApplication = accountApplication;
        }

        public SelectList Teachers { get; set; }
        public SelectList CourseCategories { get; set; }

        public void OnGet(long id)
        {
            Command = _courseApplication.GetDetails(id);
            Teachers = new SelectList(_accountApplication.GetAdmins(), "Id", "Fullname");
            CourseCategories = new SelectList(_courseCategoryApplication.GetCourseCategories(), "Id", "Name");
        }

        public IActionResult OnPost(EditCourse command)
        {
            var result = _courseApplication.Edit(command);
            if (result.IsSucceeded) return RedirectToPage("/Index");

            return RedirectToPage();
        }
    }
}
