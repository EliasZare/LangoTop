using System.Collections.Generic;
using _0_Framework.Application;

namespace LangoTop.Application.Contract.Section
{
    public interface ISectionApplication
    {
        OperationResult Create(CreateSection command);
        OperationResult Edit(EditSection command);
        OperationResult Remove(long id);
        OperationResult Restore(long id);
        EditSection GetDetails(long id);
        List<SectionViewModel> GetSections();
        List<SectionViewModel> GetSectionsBy(long courseId);
    }
}
