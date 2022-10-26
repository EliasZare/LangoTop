using System.Collections.Generic;
using _0_Framework.Application;

namespace LangoTop.Application.Contract.Account
{
    public interface IAccountApplication
    {
        OperationResult Register(RegisterAccount command);
        OperationResult Edit(EditAccount command);
        OperationResult ChangePassword(ChangePassword command);
        OperationResult ChangePasswordByCode(ChangePassword command);
        OperationResult VerifyEmail(string email);
        OperationResult Login(Login command);
        OperationResult Active(string activeCode);
        OperationResult Active(long id);
        EditAccount GetDetails(long id);
        List<AccountViewModel> GetAccounts();
        List<AccountViewModel> Search(AccountSearchModel searchModel);
        void Logout();
    }
}