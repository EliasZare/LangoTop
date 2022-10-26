using System.Collections.Generic;
using LangoTop.Application.Contract.Account;
using LangoTop.Application.Contract.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Account
{
    public class IndexModel : PageModel
    {
        private readonly IAccountApplication _accountApplication;
        private readonly IRoleApplication _roleApplication;
        public SelectList Roles;
        public AccountSearchModel SearchModel;
        public List<AccountViewModel> Accounts { get; set; }

        public IndexModel(IAccountApplication accountApplication, IRoleApplication roleApplication)
        {
            _accountApplication = accountApplication;
            _roleApplication = roleApplication;
        }

        public void OnGet(AccountSearchModel searchModel)
        {
            Roles = new SelectList(_roleApplication.GetRoles(), "Id", "Name");
            Accounts = _accountApplication.Search(searchModel);
        }

        public IActionResult OnGetRegister()
        {
            var command = new RegisterAccount
            {
                Roles = _roleApplication.GetRoles()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostRegister(RegisterAccount command)
        {
            var result = _accountApplication.Register(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var account = _accountApplication.GetDetails(id);
            account.Roles = _roleApplication.GetRoles();
            return Partial("Edit", account);
        }

        public JsonResult OnPostEdit(EditAccount command)
        {
            var result = _accountApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetChangePassword(long id)
        {
            var command = new ChangePassword { Id = id };
            return Partial("./ChangePassword", command);
        }

        public JsonResult OnPostChangePassword(ChangePassword command)
        {
            var result = _accountApplication.ChangePassword(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetActive(long id)
        {
            _accountApplication.Active(id);
            return RedirectToPage("./Index");
        }
    }
}