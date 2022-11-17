using Microsoft.AspNetCore.Http;

namespace LangoTop.Application.Contract.Banner
{
    public class CreateBanner
    {
        public string CompanyName { get; set; }
        public string Link { get; set; }
        public IFormFile BannerPicture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string Title { get; set; }
        public long Type { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
