using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class AboutBannerViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
