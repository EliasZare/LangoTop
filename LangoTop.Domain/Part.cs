using _0_Framework.Domain;

namespace LangoTop.Domain
{
    public class Part : EntityBase
    {
        public string Title { get; set; }
        public string DownloadLink { get; set; }
        public long SectionId { get; set; }
        public string Time { get; set; }
        public bool IsRemoved { get; set; }
        public Section Section { get; set; }

        public Part(string title, string downloadLink, long sectionId, string time)
        {
            Title = title;
            DownloadLink = downloadLink;
            SectionId = sectionId;
            Time = time;
            IsRemoved = false;
        }

        public void Edit(string title, string downloadLink, long sectionId, string time)
        {
            Title = title;
            DownloadLink = downloadLink;
            SectionId = sectionId;
            Time = time;
            IsRemoved = false;
        }

        public void Remove()
        {
            IsRemoved = true;
        }

        public void Restore()
        {
            IsRemoved = false;
        }
    }
}
