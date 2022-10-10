using System.Collections.Generic;
using _0_Framework.Domain;

namespace LangoTop.Domain
{
    public class Section : EntityBase
    {
        public string Title { get; set; }
        public bool IsRemoved { get; set; }
        public long CourseId { get; set; }
        public List<Part> Parts { get; set; }
        public Course Course { get; set; }

        public Section(string title, long courseId)
        {
            Title = title;
            CourseId = courseId;
            IsRemoved = false;
        }

        public void Edit(string title, long courseId)
        {
            Title = title;
            CourseId = courseId;
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
