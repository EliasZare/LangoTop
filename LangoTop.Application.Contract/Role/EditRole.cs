using System.Collections.Generic;
using _0_Framework.Infrastructure;

namespace LangoTop.Application.Contract.Role
{
    public class EditRole : CreateRole
    {
        public long Id { get; set; }
        public List<PermissionDto> MappedPermissions { get; set; }
    }
}