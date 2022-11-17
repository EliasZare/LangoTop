using System.Collections.Generic;
using _0_Framework.Application;
using _01_Query.Contracts.Course;
using _01_Query.Contracts.Order;
using LangoTop.Application.Contract.Comment;
using LangoTop.Application.Contract.Part;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class CourseModel : PageModel
    {
        private readonly ICommentApplication _commentApplication;
        private readonly ICourseQuery _courseQuery;
        private readonly IAuthHelper _authHelper;
        private readonly IOrderQuery _orderQuery;
        private readonly IPartApplication _partApplication;
        public bool IsPaid;

        //[TempData] public string DownloadLink { get; set; }
        public CourseQueryModel Course;
        public List<CourseQueryModel> LatestCourses;

        public CourseModel(ICourseQuery courseQuery, ICommentApplication commentApplication, IAuthHelper authHelper,
            IOrderQuery orderQuery, IPartApplication partApplication)
        {
            _courseQuery = courseQuery;
            _commentApplication = commentApplication;
            _authHelper = authHelper;
            _orderQuery = orderQuery;
            _partApplication = partApplication;
        }

        public void OnGet(string id)
        {
            Course = _courseQuery.GetDetails(id);
            LatestCourses = _courseQuery.LatestCourses(3);
        }

        public IActionResult OnPost(AddComment command, string articleSlug)
        {
            command.Type = CommentType.Course;
            var result = _commentApplication.AddComment(command);
            return RedirectToPage("/Course", new {Id = articleSlug});
        }

        public IActionResult OnGetDownloadFile(long partId, long courseId)
        {
            var account = _authHelper.CurrentAccountInfo();
            if (account == null) return RedirectToPage("Login");
            var paidCourses = _orderQuery.GetCoursesBy(account.Id);
            foreach (var course in paidCourses)
                if (course.Id == courseId || course.DoublePrice < 1)
                {
                    IsPaid = true;
                    break;
                }

            if (IsPaid)
            {
                var part = _partApplication.GetDetails(partId);
                if (part == null) return RedirectToPage("Login");
                return Redirect(part.DownloadLink);
            }

            return RedirectToPage("Login");
        }
    }
}
