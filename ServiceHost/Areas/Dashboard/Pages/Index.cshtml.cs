using _0_Framework.Application;
using LangoTop.Application.Contract.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Dashboard.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAuthHelper _authHelper;
        private readonly IAccountApplication _accountApplication;
        public EditAccount Account;

        public IndexModel(IAccountApplication accountApplication, IAuthHelper authHelper)
        {
            _accountApplication = accountApplication;
            _authHelper = authHelper;
        }

        public void OnGet()
        {
            Account = _accountApplication.GetDetails(_authHelper.CurrentAccountInfo().Id);
        }
    }
}
