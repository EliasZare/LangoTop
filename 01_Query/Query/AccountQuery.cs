using System.Linq;
using _01_Query.Contracts.Account;
using LangoTop.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace _01_Query.Query
{
    public class AccountQuery : IAccountQuery
    {
        private readonly LangoTopContext _context;

        public AccountQuery(LangoTopContext context)
        {
            _context = context;
        }

        public AccountQueryModel GetDetails(string username)
        {
            return _context.Accounts.Include(x => x.Role).Select(x => new AccountQueryModel
            {
                Id = x.Id,
                Fullname = x.Fullname,
                Username = x.Username,
                Email = x.Email,
                RoleId = x.RoleId,
                Biography = x.Biography,
                ProfilePhoto = x.ProfilePhoto,
                Role = x.Role.Name,
                TeacherUsername = x.Username
            }).FirstOrDefault(x => x.Username == username);
        }
    }
}
