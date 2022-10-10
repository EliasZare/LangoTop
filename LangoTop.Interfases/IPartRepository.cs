using System.Collections.Generic;
using _0_Framework.Domain;
using LangoTop.Application.Contract.Part;
using LangoTop.Domain;

namespace LangoTop.Interfaces
{
    public interface IPartRepository : IRepository<long, Part>
    {
        EditPart GetDetails(long id);
        List<PartViewModel> GetPartsBy(long sectionId);
        List<PartViewModel> GetParts();
    }
}
