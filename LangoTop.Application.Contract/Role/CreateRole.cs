using System.Collections.Generic;

namespace LangoTop.Application.Contract.Role
{
    public class CreateRole
    {
        public string Name { get; set; }
        public List<int> Permissions { get; set; }
    }
}
