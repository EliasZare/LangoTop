namespace LangoTop.Application.Contract.Part
{
    public class PartViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string DownloadLink { get; set; }
        public string Section { get; set; }
        public long SectionId { get; set; }
        public bool IsRemoved { get; set; }
        public string Course { get; set; }
        public long CourseId { get; set; }
    }
}