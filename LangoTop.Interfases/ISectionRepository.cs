using System.Collections.Generic;
using _0_Framework.Domain;
using LangoTop.Application.Contract.Section;
using LangoTop.Domain;

namespace LangoTop.Interfaces
{
    public interface ISectionRepository : IRepository<long, Section>
    {
        EditSection GetDetails(long id);
        List<SectionViewModel> GetSections();
        List<SectionViewModel> GetSectionsBy(long courseId);
    }
}
