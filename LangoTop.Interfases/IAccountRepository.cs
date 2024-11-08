﻿using System.Collections.Generic;
using _0_Framework.Domain;
using LangoTop.Application.Contract.Account;
using LangoTop.Domain;

namespace LangoTop.Interfaces
{
    public interface IAccountRepository : IRepository<long, Account>
    {
        Account GetBy(string email);
        EditAccount GetDetails(long id);
        List<AccountViewModel> Search(AccountSearchModel searchModel);
        List<AccountViewModel> GetAccounts();
        List<AccountViewModel> GetAdmins();
        Account GetByCode(string activeCode);
    }
}