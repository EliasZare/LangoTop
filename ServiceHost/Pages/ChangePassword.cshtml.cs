using LangoTop.Application.Contract.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ChangePasswordModel : PageModel
    {
        private readonly IAccountApplication _accountApplication;
        [TempData] public string Message { get; set; }
        [TempData] public string Code { get; set; }

        public ChangePasswordModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public void OnGet(string id)
        {
            var command = new ChangePassword {Code = id};
            Code = id;
        }

        public void OnPost(ChangePassword command)
        {
            command.Code = Code;
            var result = _accountApplication.ChangePasswordByCode(command);

            Message = result.Message;
            RedirectToPage("/ChangePassword", Message);
        }
    }
}
