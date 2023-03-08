using System.Collections.Generic;
using _0_Framework.Domain;
using LangoTop.Application.Contract.Page;
using LangoTop.Domain;

namespace LangoTop.Interfaces
{
    public interface IPageRepository : IRepository<long, Page>
    {
        EditPage GetDetails(long id);
        List<PageViewModel> GetPages();
        PageViewModel GetSlugBy(string key);
    }
}
