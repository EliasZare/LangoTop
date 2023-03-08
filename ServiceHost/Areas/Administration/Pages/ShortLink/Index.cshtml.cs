using System.Collections.Generic;
using LangoTop.Application.Contract.Page;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.ShortLink
{
    public class IndexModel : PageModel
    {
        private readonly IPageApplication _pageApplication;
        public List<PageViewModel> Pages;
        public string Message { get; set; }

        public IndexModel(IPageApplication pageApplication)
        {
            _pageApplication = pageApplication;
        }

        public void OnGet()
        {
            Pages = _pageApplication.GetPages();
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreatePage());
        }

        public JsonResult OnPostCreate(CreatePage command)
        {
            var result = _pageApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var course = _pageApplication.GetDetails(id);
            return Partial("./Edit", course);
        }

        public JsonResult OnPostEdit(EditPage command)
        {
            var result = _pageApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetRemove(long id)
        {
            var result = _pageApplication.Remove(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetRestore(long id)
        {
            var result = _pageApplication.Restore(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}
