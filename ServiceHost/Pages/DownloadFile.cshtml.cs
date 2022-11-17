using _0_Framework.Application;
using _01_Query.Contracts.Order;
using LangoTop.Application.Contract.Part;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    [Authorize]
    public class DownloadFileModel : PageModel
    {
        private readonly IAuthHelper _authHelper;
        private readonly IOrderQuery _orderQuery;
        private readonly IPartApplication _partApplication;
        public bool IsPaid;

        public DownloadFileModel(IAuthHelper authHelper, IOrderQuery orderQuery, IPartApplication partApplication)
        {
            _authHelper = authHelper;
            _orderQuery = orderQuery;
            _partApplication = partApplication;
        }

        [TempData] public string DownloadLink { get; set; }


        public void OnGet(long partId, long courseId)
        {
            var account = _authHelper.CurrentAccountInfo();
            var paidCourses = _orderQuery.GetCoursesBy(account.Id);
            foreach (var course in paidCourses)
                if (course.Id == courseId)
                {
                    IsPaid = true;
                    break;
                }

            if (IsPaid)
            {
                var part = _partApplication.GetDetails(account.Id);
                if (part == null)
                {
                    DownloadLink = "";
                    RedirectToPage("");
                }

                RedirectToPage(part.DownloadLink);
            }
        }
    }
}
