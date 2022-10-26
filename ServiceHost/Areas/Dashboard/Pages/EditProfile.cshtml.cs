using _0_Framework.Application;
using LangoTop.Application.Contract.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Dashboard.Pages
{
    public class EditProfileModel : PageModel
    {
        private readonly IAccountApplication _accountApplication;
        [TempData] public string SuccessMessage { get; set; }
        [TempData] public string ErrorMessage { get; set; }
        public EditAccount EditAccount = new();
        private readonly IAuthHelper _authHelper;

        public EditProfileModel(IAccountApplication accountApplication, IAuthHelper authHelper)
        {
            _accountApplication = accountApplication;
            _authHelper = authHelper;
        }

        public void OnGet()
        {
            EditAccount = _accountApplication.GetDetails(_authHelper.CurrentAccountInfo().Id);
        }

        public IActionResult OnPost(EditAccount command)
        {
            var result = _accountApplication.Edit(command);
            if (result.IsSucceeded)
                SuccessMessage = result.Message;
            else
                ErrorMessage = result.Message;
            return RedirectToPage("/EditProfile");
        }
    }
}
