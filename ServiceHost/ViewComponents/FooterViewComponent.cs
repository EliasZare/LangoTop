using System.Collections.Generic;
using _01_Query.Contracts;
using _01_Query.Contracts.Banner;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly IBannerQuery _bannerQuery;
        public List<BannerQueryModel> Banners;

        public FooterViewComponent(IBannerQuery bannerQuery)
        {
            _bannerQuery = bannerQuery;
        }

        public IViewComponentResult Invoke()
        {
            var command = new FooterQueryModel
            {
                Banners = _bannerQuery.GetFooterLinks()
            };
            return View(command);
        }
    }
}
