using System;
using _0_Framework.Application;
using LangoTop.Application.Contract.Page;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class pModel : PageModel
    {
        private readonly IPageApplication _pageApplication;
        public Uri uri;

        public pModel(IPageApplication pageApplication)
        {
            _pageApplication = pageApplication;
        }

        public IActionResult OnGet(string key)
        {
            var page = _pageApplication.GetSlugBy(key);
            if (!string.IsNullOrWhiteSpace(page.Slug))
            {
                switch (page.Type)
                {
                    case PageTypes.Article:
                        uri = new Uri($"https://langotop.ir/Article/{page.Slug}");
                        break;
                    case PageTypes.Course:
                        uri = new Uri($"https://langotop.ir/Course/{page.Slug}");
                        break;
                    case PageTypes.Page:
                        uri = new Uri($"https://langotop.ir/{page.Slug}");
                        break;
                    default:
                        uri = new Uri("https://langotop.ir");
                        break;
                }

                return Redirect(uri.AbsoluteUri);
            }

            return RedirectToPage("Index");
        }
    }
}
