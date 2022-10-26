using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class ServicesViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
