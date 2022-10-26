using System.Collections.Generic;
using _01_Query.Contracts.Part;

namespace _01_Query.Contracts.Section
{
    public class SectionQueryModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long CourseId { get; set; }
        public bool IsRemoved { get; set; }
        public List<PartQueryModel> Parts { get; set; }
    }
}
