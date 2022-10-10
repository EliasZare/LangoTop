using System.Collections.Generic;
using LangoTop.Application.Contract.Course;
using LangoTop.Application.Contract.Part;
using LangoTop.Application.Contract.Section;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Courses.Part
{
    public class IndexModel : PageModel
    {
        private readonly IPartApplication _partApplication;
        private readonly ISectionApplication _sectionApplication;
        private readonly ICourseApplication _courseApplication;

        [TempData] public string CourseId { get; set; }

        public List<PartViewModel> Parts;
        public SelectList Sections;
        public string Message { get; set; }

        public IndexModel(IPartApplication partApplication, ISectionApplication sectionApplication,
            ICourseApplication courseApplication)
        {
            _partApplication = partApplication;
            _sectionApplication = sectionApplication;
            _courseApplication = courseApplication;
        }

        public void OnGet(long sectionId, long courseId)
        {
            Parts = _partApplication.GetPartsBy(sectionId);

            CourseId = courseId.ToString();
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreatePart
            {
                Sections = _sectionApplication.GetSectionsBy(int.Parse(CourseId))
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreatePart command)
        {
            var result = _partApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var part = _partApplication.GetDetails(id);
            part.Sections = _sectionApplication.GetSectionsBy(int.Parse(CourseId));
            return Partial("./Edit", part);
        }

        public JsonResult OnPostEdit(EditPart command)
        {
            var result = _partApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetRemove(long id)
        {
            var result = _partApplication.Remove(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetRestore(long id)
        {
            var result = _partApplication.Restore(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}
