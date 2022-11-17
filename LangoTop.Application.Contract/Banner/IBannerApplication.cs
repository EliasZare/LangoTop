using System.Collections.Generic;
using _0_Framework.Application;

namespace LangoTop.Application.Contract.Banner
{
    public interface IBannerApplication
    {
        OperationResult Create(CreateBanner command);
        OperationResult Edit(EditBanner command);
        OperationResult Remove(long id);
        OperationResult Restore(long id);
        List<BannerViewModel> GetBanners();
        EditBanner GetDetails(long id);
    }
}
