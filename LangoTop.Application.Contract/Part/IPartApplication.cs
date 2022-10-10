using System.Collections.Generic;
using _0_Framework.Application;

namespace LangoTop.Application.Contract.Part
{
    public interface IPartApplication
    {
        OperationResult Create(CreatePart command);
        OperationResult Edit(EditPart command);
        OperationResult Remove(long id);
        OperationResult Restore(long id);
        EditPart GetDetails(long id);
        List<PartViewModel> GetPartsBy(long sectionId);
        List<PartViewModel> GetParts();
    }
}
