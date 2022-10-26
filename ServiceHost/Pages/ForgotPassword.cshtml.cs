using LangoTop.Application.Contract.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly IAccountApplication _accountApplication;
        [TempData] public string Message { get; set; }

        public ForgotPasswordModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost(string email)
        {
            var result = _accountApplication.VerifyEmail(email);
            Message = result.Message;
            return RedirectToPage("/ForgotPassword");
        }
    }
}
