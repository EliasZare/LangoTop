using System.Collections.Generic;
using LangoTop.Application.Contract.Section;

namespace LangoTop.Application.Contract.Part
{
    public class CreatePart
    {
        public string Title { get; set; }
        public string DownloadLink { get; set; }
        public long SectionId { get; set; }
        public List<SectionViewModel> Sections { get; set; }
        public string Time { get; set; }
    }
}
