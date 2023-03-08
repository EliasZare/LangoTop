namespace LangoTop.Application.Contract.Page
{
    public class PageViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string ShortKey { get; set; }
        public string CreationDate { get; set; }
        public bool IsRemoved { get; set; }
        public int Type { get; set; }
    }
}