using System.Collections.Generic;
using _0_Framework.Application;
using LangoTop.Application.Contract.Banner;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Ads.Banner
{
    public class IndexModel : PageModel
    {
        private readonly IBannerApplication _bannerApplication;
        private AdsType _adsType = new();
        public List<BannerViewModel> Banners { get; set; }
        public string Message { get; set; }

        public IndexModel(IBannerApplication bannerApplication)
        {
            _bannerApplication = bannerApplication;
        }


        public void OnGet()
        {
            Banners = _bannerApplication.GetBanners();
            foreach (var banner in Banners) banner.StrType = _adsType.GetName(banner.Type.ToString());
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateBanner());
        }

        public JsonResult OnPostCreate(CreateBanner command)
        {
            var result = _bannerApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var course = _bannerApplication.GetDetails(id);
            return Partial("./Edit", course);
        }

        public JsonResult OnPostEdit(EditBanner command)
        {
            var result = _bannerApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetRemove(long id)
        {
            var result = _bannerApplication.Remove(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetRestore(long id)
        {
            var result = _bannerApplication.Restore(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}
