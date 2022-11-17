using System;
using _0_Framework.Infrastructure;
using LangoTop.Application.Contract.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class SignupModel : PageModel
    {
        [TempData] public string RegisterMessage { get; set; }
        [TempData] public string RegisterMessageSuccess { get; set; }
        private readonly IAccountApplication _accountApplication;

        public SignupModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost(RegisterAccount command)
        {
            command.RoleId = Convert.ToInt32(Roles.SystemUser);
            var result = _accountApplication.Register(command);

            if (result.IsSucceeded)
            {
                RegisterMessageSuccess = result.Message;
                return RedirectToPage("/Signup");
            }

            RegisterMessage = result.Message;
            return RedirectToPage("/Signup");
        }
    }
}
