using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class LandingViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
