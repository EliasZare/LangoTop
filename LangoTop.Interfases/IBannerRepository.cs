using System.Collections.Generic;
using _0_Framework.Domain;
using LangoTop.Application.Contract.Banner;
using LangoTop.Domain;

namespace LangoTop.Interfaces
{
    public interface IBannerRepository : IRepository<long, Banner>
    {
        List<BannerViewModel> GetBanners();
        EditBanner GetDetails(long id);
    }
}