using LangoTop.Application.Contract.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ContactusModel : PageModel
    {
        private readonly ICommentApplication _commentApplication;

        public ContactusModel(ICommentApplication commentApplication)
        {
            _commentApplication = commentApplication;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost(AddComment command)
        {
            command.Type = CommentType.Course;
            var result = _commentApplication.AddComment(command);
            return RedirectToPage("/contactus");
        }
    }
}
