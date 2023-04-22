using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Application.Email;
using LangoTop.Application.Contract.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class LoginModel : PageModel
    {
        [TempData] public string LoginMessage { get; set; }
        private readonly IAccountApplication _accountApplication;
        private readonly ICaptchaValidator _captchaValidator;
        private IEmailService _emailService;

        public LoginModel(IAccountApplication accountApplication, ICaptchaValidator captchaValidator,
            IEmailService emailService)
        {
            _accountApplication = accountApplication;
            _captchaValidator = captchaValidator;
            _emailService = emailService;
        }

        public void OnGet()
        {
        }

        public async Task<RedirectToPageResult> OnPost(Login command, string captcha)
        {
            var captchaResult = await _captchaValidator.IsCaptchaPassedAsync(captcha);

            //ModelState.AddModelError("captcha", "Captcha validation failed");
            if (captchaResult)
            {
                var result = _accountApplication.Login(command);
                if (result.IsSucceeded) return RedirectToPage("/Index");
            }
            else
            {
                LoginMessage = "کپچا معتبر نیست!";
                return RedirectToPage("Login");
            }

            return RedirectToPage("Login");
        }

        public IActionResult OnGetLogout()
        {
            _accountApplication.Logout();
            return RedirectToPage("/Index");
        }
    }
}
