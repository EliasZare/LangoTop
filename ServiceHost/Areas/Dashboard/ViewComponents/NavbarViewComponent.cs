using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Areas.Dashboard.ViewComponents
{
    public class NavbarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
