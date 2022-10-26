using System.Collections.Generic;
using _0_Framework.Domain;
using LangoTop.Application.Contract.Role;
using LangoTop.Domain;

namespace LangoTop.Interfaces
{
    public interface IRoleRepository : IRepository<long, Role>
    {
        EditRole GetDetails(long id);
        List<RoleViewModel> GetRoles();
    }
}
