using System.Collections.Generic;
using _0_Framework.Application;

namespace LangoTop.Application.Contract.Page
{
    public interface IPageApplication
    {
        OperationResult Create(CreatePage command);
        OperationResult Edit(EditPage command);
        OperationResult Remove(long id);
        OperationResult Restore(long id);
        EditPage GetDetails(long id);
        PageViewModel GetSlugBy(string key);
        List<PageViewModel> GetPages();
    }
}
