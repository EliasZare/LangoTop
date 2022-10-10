using System.Collections.Generic;
using LangoTop.Application.Contract.Course;
using LangoTop.Application.Contract.Section;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Courses.Section
{
    public class IndexModel : PageModel
    {
        private readonly ISectionApplication _sectionApplication;
        private readonly ICourseApplication _courseApplication;
        public List<SectionViewModel> Sections { get; set; }
        public string Message { get; set; }
        public SelectList Courses;

        public IndexModel(ISectionApplication sectionApplication, ICourseApplication courseApplication)
        {
            _sectionApplication = sectionApplication;
            _courseApplication = courseApplication;
        }


        public void OnGet(long id)
        {
            Sections = _sectionApplication.GetSectionsBy(id);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateSection
            {
                Courses = _courseApplication.GetCourses()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateSection command)
        {
            var result = _sectionApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var course = _sectionApplication.GetDetails(id);
            course.Courses = _courseApplication.GetCourses();
            return Partial("./Edit", course);
        }

        public JsonResult OnPostEdit(EditSection command)
        {
            var result = _sectionApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetRemove(long id)
        {
            var result = _sectionApplication.Remove(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetRestore(long id)
        {
            var result = _sectionApplication.Restore(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}
