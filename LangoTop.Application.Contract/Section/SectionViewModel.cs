using System.Collections.Generic;
using LangoTop.Application.Contract.Part;

namespace LangoTop.Application.Contract.Section
{
    public class SectionViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Course { get; set; }
        public long CourseId { get; set; }
        public bool IsRemoved { get; set; }
        public List<PartViewModel> Parts { get; set; }
        public string CreationDate { get; set; }
    }
}