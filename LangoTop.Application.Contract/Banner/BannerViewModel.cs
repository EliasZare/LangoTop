namespace LangoTop.Application.Contract.Banner
{
    public class BannerViewModel
    {
        public long Id { get; set; }
        public string CompanyName { get; set; }
        public string Link { get; set; }
        public string BannerPicture { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string Title { get; set; }
        public long Type { get; set; }
        public string StrType { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool IsRemoved { get; set; }
    }
}