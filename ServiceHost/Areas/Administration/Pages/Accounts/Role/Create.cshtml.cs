using _0_Framework.Infrastructure;
using LangoTop.Application.Contract.Role;
using LangoTop.Infrastructure.Repository.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Role
{
    public class CreateModel : PageModel
    {
        public CreateRole Command;
        private readonly IRoleApplication _roleApplication;

        public CreateModel(IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
        }

        [NeedsPermission(AccountPermissions.CreateRoles)]
        public void OnGet()
        {
        }

        [NeedsPermission(AccountPermissions.CreateRoles)]
        public IActionResult OnPost(CreateRole command)
        {
            var result = _roleApplication.Create(command);
            return RedirectToPage("Index");
        }
    }
}