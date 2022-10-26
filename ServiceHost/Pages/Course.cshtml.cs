using System.Collections.Generic;
using _01_Query.Contracts.Course;
using LangoTop.Application.Contract.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class CourseModel : PageModel
    {
        private readonly ICommentApplication _commentApplication;
        private readonly ICourseQuery _courseQuery;
        public CourseQueryModel Course;
        public List<CourseQueryModel> LatestCourses;

        public CourseModel(ICourseQuery courseQuery, ICommentApplication commentApplication)
        {
            _courseQuery = courseQuery;
            _commentApplication = commentApplication;
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
    }
}
