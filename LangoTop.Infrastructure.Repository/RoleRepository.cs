using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using LangoTop.Application.Contract.Role;
using LangoTop.Domain;
using LangoTop.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LangoTop.Infrastructure.Repository
{
    public class RoleRepository : RepositoryBase<long, Role>, IRoleRepository
    {
        private readonly LangoTopContext _context;

        public RoleRepository(LangoTopContext context) : base(context)
        {
            _context = context;
        }


        public EditRole GetDetails(long id)
        {
            var role = _context.Roles.Select(x => new EditRole
            {
                Id = x.Id,
                Name = x.Name,
                MappedPermissions = MapPermission(x.Permissions)
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);

            role.Permissions = role.MappedPermissions.Select(x => x.Code).ToList();

            return role;
        }

        private static List<PermissionDto> MapPermission(List<Permission> permissions)
        {
            return permissions.Select(x => new PermissionDto(x.Code, x.Name)).ToList();
        }

        public List<RoleViewModel> GetRoles()
        {
            return _context.Roles.Select(x => new RoleViewModel
            {
                Id = x.Id,
                Name = x.Name,
                CreationDate = x.CreationDate.ToFarsi()
            }).OrderByDescending(x => x.Id).ToList();
        }
    }
}
