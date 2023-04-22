using System.Collections.Generic;
using LangoTop.Application.Contract.Image;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.ImageManager
{
    public class IndexModel : PageModel
    {
        private readonly IImageApplication _imageApplication;
        public List<ImageViewModel> Images { get; set; }

        public IndexModel(IImageApplication imageApplication)
        {
            _imageApplication = imageApplication;
        }

        public void OnGet()
        {
            Images = _imageApplication.GetImages();
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateImage());
        }

        public JsonResult OnPostCreate(CreateImage command)
        {
            var result = _imageApplication.Create(command);
            return new JsonResult(result.Message);
        }
    }
}
